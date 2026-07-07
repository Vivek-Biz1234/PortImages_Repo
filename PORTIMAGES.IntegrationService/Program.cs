using PORTIMAGES.Common.Helpers;
using PORTIMAGES.IntegrationService;
using PORTIMAGES.IntegrationService.Jobs;
using PORTIMAGES.IntegrationService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

#region Added Manually 
builder.Services.AddScoped<MasterDataSyncJob>();
builder.Services.AddScoped<ExternalDbService>();
builder.Services.AddScoped<SyncService>();


builder.Services.AddScoped<FileHelper>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var rootPath = config.GetValue<string>("FileSettings:RootPath");

    if (string.IsNullOrEmpty(rootPath))
    {
        rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    }

    return new FileHelper(rootPath);
});

#endregion

var host = builder.Build();
host.Run();
