namespace PhotoMetadata;

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string configFile)
    {
        CreateHostBuilder(configFile).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string configFile) =>
        Helpers.CreateSimpleBuilder(configFile)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(60));
                });
}
