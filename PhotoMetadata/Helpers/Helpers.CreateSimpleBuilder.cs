namespace PhotoMetadata;

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal static partial class Helpers
{
    internal static IHostBuilder CreateSimpleBuilder(string instanceConfigPath)
    {
        HostBuilder result = new();

        result.UseContentRoot(Directory.GetCurrentDirectory());

        result.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true);

            if (string.IsNullOrWhiteSpace(instanceConfigPath) == false)
            {
                config.AddJsonFile(instanceConfigPath, optional: false);
            }
        })
        .ConfigureLogging((hostingContext, logging) =>
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventSourceLogger();
        })
        .UseDefaultServiceProvider((context, options) =>
        {
            var isDevelopment = context.HostingEnvironment.IsDevelopment();
            options.ValidateScopes = isDevelopment;
            options.ValidateOnBuild = isDevelopment;
        });

        return result;
    }
}
