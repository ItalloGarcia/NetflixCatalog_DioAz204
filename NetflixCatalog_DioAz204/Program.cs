using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetflixCatalog_DioAz204.Func.Services;
using NetflixCatalog_DioAz204.Func.Config;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>();
        services.Configure<CosmosDbOption>(options =>
        {
            options.Endpoint = Environment.GetEnvironmentVariable("CosmosDB_Endpoint");
            options.AccountKey = Environment.GetEnvironmentVariable("CosmosDB_Key");
            options.DataBaseName = Environment.GetEnvironmentVariable("CosmosDB_Database");
        });
    })
    .Build();

host.Run();
