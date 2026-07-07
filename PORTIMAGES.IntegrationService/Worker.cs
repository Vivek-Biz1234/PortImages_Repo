using PORTIMAGES.IntegrationService.Jobs;

namespace PORTIMAGES.IntegrationService
{
    public class Worker : BackgroundService
    { 
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<Worker> _logger;
        public Worker(IServiceScopeFactory scopeFactory, IConfiguration config, ILogger<Worker> logger)
        {
            _scopeFactory = scopeFactory;
            _config = config;
            _logger = logger;
        } 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var intervalInMinutes = _config.GetValue<int>("SyncSettings:IntervalInMinutes");
            var intervalInHours = _config.GetValue<int>("SyncSettings:IntervalInHours");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var job = scope.ServiceProvider.GetRequiredService<MasterDataSyncJob>();
                    //await job.SyncCountryAsync();
                    //await job.SyncPortAsync();
                    //await job.SyncTerminalAsync();
                    //await job.SyncCategoryAsync();
                    //await job.SyncShippingAsync();
                    //await job.SyncShipAsync();
                    //await job.SyncMakerAsync();
                    //await job.SyncModelAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while running sync job");
                }
                _logger.LogInformation("Waiting for next execution...");
                //await Task.Delay(TimeSpan.FromSeconds(intervalInMinutes), stoppingToken);
                await Task.Delay(TimeSpan.FromHours(intervalInHours), stoppingToken);
            } 
        }
    }
}
