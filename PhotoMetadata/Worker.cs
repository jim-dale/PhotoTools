using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PhotoMetadata
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _lifetime;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _configuration = configuration;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var context = new Context();

            _configuration.Bind(AppOptions.Name, context.Options);

            context.Items = Helpers.GetPhotoContextsFromImageFiles(context.Options.InputPath, context.Options.Includes, context.Options.Excludes);

            context.CurrentEntries = Helpers.LoadAllMetadataFromExcelFiles(context.Options.ExcelFiles);
            context.PreviousEntries = Helpers.LoadAllMetadataFromJsonFile(context.Options.MetadataStorePath);

            context.Changes = Helpers.GetNewAndUpdatedMetadata(context.CurrentEntries, context.PreviousEntries);

            var merged = from item in context.Items
                         let current = context.CurrentEntries.SingleOrDefault(i => i.PhotoId == item.PhotoId)
                         let previous = context.PreviousEntries.SingleOrDefault(i => i.PhotoId == item.PhotoId)
                         let changed = context.Changes.SingleOrDefault(i => i.PhotoId == item.PhotoId)
                         select new PhotoContext
                         {
                             Info = item,
                             Current = current,
                             Previous = previous,
                             Changed = changed,
                             Output = current?.TryConvertToOutputMetadata()
                         };

            if (merged.Any())
            {
                var exiftool = new ConsoleClient(context.Options.ExiftoolPath);
                var imagemagick = new ConsoleClient(context.Options.ImageMagickPath);

                _logger.LogInformation("Number of items: {ItemCount}", merged.Count());

                foreach (var item in merged)
                {
                    // This delay seems to allow the Ctrl-C handler to operate correctly to
                    // close the app in a controlled way. Without it the app does not close
                    // in a timely manner, if at all, when Ctrl-C is pressed.
                    await Task.Delay(100, stoppingToken);

                    if (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }

                    var inputFile = item.Info.PhotoPath;
                    var metadataFile = item.Info.FileName + ".json";
                    var outputFile = Path.Combine(context.Options.OutputPath, item.Info.RelativePath, item.Info.FileName + ".jpg");

                    bool processFile = (context.Options.Initialise) 
                        ? (File.Exists(outputFile) == false || context.Options.Force)
                        : (File.Exists(outputFile) == false || item.Output != null || context.Options.Force);

                    if (processFile)
                    {
                        _logger.LogInformation("{FilmId},{PhotoId},'{PhotoPath}'", item.Info.FilmId, item.Info.PhotoId, item.Info.PhotoPath);

                        imagemagick.ResizeAndWriteMainImage(inputFile, outputFile);

                        if (item.Output != null && File.Exists(outputFile))
                        {
                            await Helpers.WriteToJsonFile(metadataFile, item.Output);

                            exiftool.AddPhotoMetadata(outputFile, metadataFile);
                        }
                    }
                }
            }
            if (stoppingToken.IsCancellationRequested == false)
            {
                await Helpers.WriteToJsonFile(context.Options.MetadataStorePath, context.CurrentEntries);
            }

            _lifetime.StopApplication();
        }
    }
}
