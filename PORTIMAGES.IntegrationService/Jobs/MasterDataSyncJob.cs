using PORTIMAGES.Common.Helpers;
using PORTIMAGES.IntegrationService.Services;
using System.Data;

namespace PORTIMAGES.IntegrationService.Jobs
{
    public class MasterDataSyncJob
    {
        private readonly ILogger<MasterDataSyncJob> _logger;
        private readonly SyncService _syncService;
        private readonly ExternalDbService _externalDbService;
        private readonly FileHelper _fileHelper;
        string allowedExtensions = ".jpg,.png,.jpeg,.webp";
        public MasterDataSyncJob(ILogger<MasterDataSyncJob> logger, ExternalDbService externalDbService, SyncService syncService, FileHelper fileHelper)
        {
            _logger = logger;
            _externalDbService = externalDbService;
            _syncService = syncService;
            _fileHelper = fileHelper;
        }

        #region Sync Countries For Country Master
        public async Task SyncCountryAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Country Sync Started");
                var data = await _externalDbService.GetCountryDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                //Clear staging
                await _syncService.ClearCountryStagingAsync();

                //Bulk insert
                await _syncService.BulkInsertCountryAsync(data);

                //Merge
                await _syncService.MergeCountryAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Ports For Port Master
        public async Task SyncPortAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Port Sync Started");
                var data = await _externalDbService.GetPortDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                await _syncService.ClearPortStagingAsync();
                await _syncService.BulkInsertPortAsync(data);
                await _syncService.MergePortAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Port Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Terminals For Terminal Master
        public async Task SyncTerminalAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Terminal Sync Started");
                var data = await _externalDbService.GetTerminalDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                await _syncService.ClearTerminalStagingAsync();
                await _syncService.BulkInsertTerminalAsync(data);
                await _syncService.MergeTerminalAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Terminal Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Categories For Category Master
        public async Task SyncCategoryAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Category Sync Started");
                var data = await _externalDbService.GetCategoryDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                await _syncService.ClearCategoryStagingAsync();
                await _syncService.BulkInsertCategoryAsync(data);
                await _syncService.MergeCategoryAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Category Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Shipping For Shipping Master
        public async Task SyncShippingAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Shipping Sync Started");
                var data = await _externalDbService.GetShippingDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                await _syncService.ClearShippingStagingAsync();
                await _syncService.BulkInsertShippingAsync(data);
                await _syncService.MergeShippingAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Shipping Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Ship For Ship Master
        public async Task SyncShipAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Ship Sync Started");
                var data = await _externalDbService.GetShipDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                await _syncService.ClearShipStagingAsync();
                await _syncService.BulkInsertShipAsync(data);
                await _syncService.MergeShipAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ship Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Makers For Maker Master
        public async Task SyncMakerAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Maker Sync Started");
                var data = await _externalDbService.GetMakerDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                }
                data.Columns["Logo"].ReadOnly = false;
                foreach (DataRow row in data.Rows)
                {
                    try
                    {
                        var fileName = row["Logo"]?.ToString();
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            var filePath = await _fileHelper.SaveFileFromUrlAsync(fileName, "MakerLogos");
                            row["Logo"]=filePath??string.Empty;
                        }
                        else { row["Logo"] = string.Empty; }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "File processing failed for Maker: {Maker}", row["MakerName"]);
                        row["Logo"] = string.Empty;
                    }
                }  

                await _syncService.ClearMakerStagingAsync();
                await _syncService.BulkInsertMakerAsync(data);
                await _syncService.MergeMakerAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Maker Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
        #region Sync Models For Model Master
        public async Task SyncModelAsync()
        {
            var errorId = Guid.NewGuid().ToString("N");
            try
            {
                _logger.LogInformation("Model Sync Started");
                var data = await _externalDbService.GetModelDataAsync();
                if (data.Rows.Count == 0)
                {
                    _logger.LogWarning("No data found");
                    return;
                } 
                await _syncService.ClearModelStagingAsync();
                await _syncService.BulkInsertModelAsync(data);
                await _syncService.MergeModelAsync();
                _logger.LogInformation($"Inserted {data.Rows.Count} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Maker Sync Failed | ErrorId: {ErrorId}", errorId);
            }
        }
        #endregion
    }
}
