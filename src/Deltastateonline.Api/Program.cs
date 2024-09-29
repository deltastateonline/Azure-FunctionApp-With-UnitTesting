using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using tfi_test03;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()

    .ConfigureServices(services => {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        services.AddApplication(configuration);


        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();


        services.Configure<KestrelServerOptions>(options =>{
            options.AllowSynchronousIO  = true;
        });
    })
    .Build();

host.Run();
