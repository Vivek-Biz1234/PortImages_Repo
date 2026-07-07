using Microsoft.Data.SqlClient;
using PORTIMAGES.IntegrationService.Helpers;
using System.Data;

namespace PORTIMAGES.IntegrationService.Services
{
    public class SyncService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public SyncService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("MainDB");
        }
        #region For Country Master

        //Clear Staging
        public async Task ClearCountryStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.CountryMaster_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }

        //Bulk Insert into Staging
        public async Task BulkInsertCountryAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.CountryMaster_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("CountryName", "CountryName");
            bulk.ColumnMappings.Add("ISDCode", "ISDCode");
            await bulk.WriteToServerAsync(dataTable);
        }

        //MERGE (Insert + Update)
        public async Task MergeCountryAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"
                            MERGE dbo.CountryMaster AS target
                            USING staging.CountryMaster_Staging AS source
                            ON target.CountryName = source.CountryName 

                            WHEN MATCHED THEN
                                UPDATE SET 
                                    target.ISDCode = source.ISDCode,
                                    target.UpdatedOn = GETDATE(),
                                    target.UpdatedBy = {systemEmpId}

                            WHEN NOT MATCHED THEN
                                INSERT (CountryName,ISDCode, IsActive, CreatedBy, CreatedOn)
                                VALUES (source.CountryName,source.ISDCode, 1, {systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion
        #region For Terminal Master

        public async Task ClearTerminalStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.TerminalMaster_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }

        public async Task BulkInsertTerminalAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.TerminalMaster_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("PortName", "PortName");
            bulk.ColumnMappings.Add("Tname", "TerminalName");
            await bulk.WriteToServerAsync(dataTable);
        }

        public async Task MergeTerminalAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"
                            MERGE dbo.TerminalMaster AS target
                            USING (
	                            SELECT PM.ID AS PortID,TM.TerminalName FROM staging.TerminalMaster_Staging TM
	                            LEFT JOIN dbo.PortMaster PM ON PM.PortName=TM.PortName
                            ) AS source
                            ON target.TerminalName=source.TerminalName

                            WHEN MATCHED THEN
	                            UPDATE SET
		                            target.PortId =source.PortId,
		                            target.TerminalName=source.TerminalName,
		                            target.UpdatedOn = GETDATE(),
		                            target.UpdatedBy = {systemEmpId}

                            WHEN NOT MATCHED THEN
	                            INSERT(PortID,TerminalName,IsActive, CreatedBy, CreatedOn)
	                            VALUES(source.PortId,source.TerminalName,1,{systemEmpId},GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion    
        #region For Port Master 
        public async Task ClearPortStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.PortMaster_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertPortAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.PortMaster_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("CountryName", "CountryName");
            bulk.ColumnMappings.Add("PortName", "PortName");
            bulk.ColumnMappings.Add("SName", "SName");
            bulk.ColumnMappings.Add("Email", "Email");
            bulk.ColumnMappings.Add("Contact", "Contact");
            bulk.ColumnMappings.Add("Fax", "Fax");
            bulk.ColumnMappings.Add("Address", "Address");
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergePortAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"
                            MERGE dbo.PortMaster AS target
                            USING (
                                SELECT 
                                    s.PortName,
                                    s.SName,
                                    s.Email,
                                    s.Contact,
                                    s.Fax,
                                    s.Address,
                                    c.ID AS CountryId
                                FROM staging.PortMaster_Staging s
                                LEFT JOIN dbo.CountryMaster c 
                                    ON c.CountryName = s.CountryName
                            ) AS source
                            ON target.PortName = source.PortName

                            WHEN MATCHED THEN
                                UPDATE SET
                                    target.SName = source.SName,
                                    target.Email = ISNULL(source.Email,''),
                                    target.Contact = ISNULL(source.Contact,''),
                                    target.Fax = ISNULL(source.Fax,''),
                                    target.Address = ISNULL(source.Address,''),
                                    target.CountryId = source.CountryId,
                                    target.UpdatedOn = GETDATE(),
                                    target.UpdatedBy = {systemEmpId}
                            WHEN NOT MATCHED THEN
                                INSERT (PortName, SName, Email, Contact, Fax, Address, CountryId, CreatedBy, CreatedOn)
                                VALUES (source.PortName, source.SName, ISNULL(source.Email,''), ISNULL(source.Contact,''), ISNULL(source.Fax,''),ISNULL(source.Address,''), source.CountryId, {systemEmpId}, GETDATE());";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion
        #region For Category Master 
        public async Task ClearCategoryStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.Category_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertCategoryAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.Category_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("CategoryName", "CategoryName");
            bulk.ColumnMappings.Add("Title", "Title");
            bulk.ColumnMappings.Add("Keyword", "Keyword");
            bulk.ColumnMappings.Add("Description", "Description");
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergeCategoryAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"
                            MERGE dbo.Category AS target
                            USING staging.Category_Staging AS source
                            ON target.CategoryName = source.CategoryName 

                            WHEN MATCHED THEN
                                UPDATE SET                                    
                                    target.Title = source.Title,
                                    target.Keyword = source.Keyword,
                                    target.Description = source.Description,
                                    target.UpdatedOn = GETDATE(),
                                    target.UpdatedBy = {systemEmpId}

                            WHEN NOT MATCHED THEN
                                INSERT (CategoryName,Title,Keyword,Description, IsActive, CreatedBy, CreatedOn)
                                VALUES (source.CategoryName,source.Title,source.Keyword,source.Description, 1, {systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion  
        #region For Shipping Master 
        public async Task ClearShippingStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.ShippingMaster_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertShippingAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.ShippingMaster_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("CountryName", "CountryName");
            bulk.ColumnMappings.Add("ShippingName", "ShippingName");
            bulk.ColumnMappings.Add("Email", "Email");
            bulk.ColumnMappings.Add("Contact", "Contact");
            bulk.ColumnMappings.Add("Fax", "Fax");
            bulk.ColumnMappings.Add("PasswordHash", "PasswordHash");
            bulk.ColumnMappings.Add("CCMail", "CCMail");
            bulk.ColumnMappings.Add("HOAddress", "HOAddress");
            bulk.ColumnMappings.Add("BOAddress", "BOAddress");
            bulk.ColumnMappings.Add("PersonInCharge", "PersonInCharge");
            bulk.ColumnMappings.Add("Rate", "Rate");
            bulk.ColumnMappings.Add("OpeningBal", "OpeningBal");
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergeShippingAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"MERGE dbo.ShippingMaster AS target
                        USING (
                            SELECT 
                                s.ShippingName, 
                                s.Email,
                                s.Contact,
                                s.Fax,
		                        s.PasswordHash,
		                        s.CCMail,
		                        s.HOAddress,
		                        s.BOAddress,
                                s.PersonInCharge,
		                        s.Rate,
		                        s.OpeningBal,
                                c.ID AS CountryId
                            FROM staging.ShippingMaster_Staging s
                            LEFT JOIN dbo.CountryMaster c 
                                ON c.CountryName = s.CountryName
                        ) AS source
                        ON target.ShippingName = source.ShippingName

                        WHEN MATCHED THEN
                            UPDATE SET 
                                target.Email = ISNULL(source.Email,''),
                                target.Contact = ISNULL(source.Contact,''),
                                target.Fax = ISNULL(source.Fax,''), 
		                        target.PasswordHash = ISNULL(source.PasswordHash,''),
		                        target.CCMail = ISNULL(source.CCMail,''),
		                        target.HOAddress = ISNULL(source.HOAddress,''),
		                        target.BOAddress = ISNULL(source.BOAddress,''),
		                        target.PersonInCharge = ISNULL(source.PersonInCharge,''),
		                        target.Rate = ISNULL(source.Rate,''),
		                        target.OpeningBal = ISNULL(source.OpeningBal,''),
                                target.CountryId = source.CountryId,
                                target.UpdatedOn = GETDATE(),
                                target.UpdatedBy = {systemEmpId}
                        WHEN NOT MATCHED THEN
                            INSERT (CountryId,ShippingName, Email, Contact, Fax, PasswordHash, CCMail, HOAddress,BOAddress
	                        ,PersonInCharge, Rate,OpeningBal,CreatedBy, CreatedOn)
                            VALUES (source.CountryId,source.ShippingName,ISNULL(source.Email,''), ISNULL(source.Contact,''), ISNULL(source.Fax,'')
	                        ,ISNULL(source.PasswordHash,''),ISNULL(source.CCMail,''),ISNULL(source.HOAddress,''),ISNULL(source.BOAddress,'')
	                        ,ISNULL(source.PersonInCharge,''),ISNULL(source.Rate,''),ISNULL(source.OpeningBal,''),{systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion  

        #region For Ship Master 
        public async Task ClearShipStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.ShipMaster_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertShipAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.ShipMaster_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            };
            bulk.ColumnMappings.Add("ShipName", "ShipName"); 
            bulk.ColumnMappings.Add("ShipType", "ShipType"); 
            bulk.ColumnMappings.Add("Shipuse", "Shipuse"); 
            bulk.ColumnMappings.Add("ShippingName", "ShippingName"); 
            bulk.ColumnMappings.Add("PortName", "PortName"); 
            bulk.ColumnMappings.Add("TerminalName", "TerminalName"); 
            bulk.ColumnMappings.Add("CountryName", "CountryName"); 
            bulk.ColumnMappings.Add("DepDate", "DepDate"); 
            bulk.ColumnMappings.Add("ArrDate", "ArrDate"); 
            bulk.ColumnMappings.Add("Freight", "Freight"); 
            bulk.ColumnMappings.Add("LCapacity", "LCapacity"); 
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergeShipAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"MERGE dbo.ShipMaster AS target
                            USING (
                                SELECT   
		                            SM.ShipName,
		                            ST.ID AS ShipTypeId,
		                            SU.ID AS ShipUseId,
		                            SP.ID AS ShippingId,
		                            PM.ID AS PortId,
		                            TM.ID AS TerminalId,
                                    C.ID AS CountryId,
		                            SM.DepDate,
		                            SM.ArrDate,
		                            SM.Freight,
		                            SM.LCapacity
		                            FROM staging.ShipMaster_Staging SM
		                            LEFT JOIN dbo.ShipType ST ON ST.TypeName=SM.ShipType
		                            LEFT JOIN dbo.ShipUse SU ON SU.UseType=SM.ShipUse
		                            LEFT JOIN dbo.ShippingMaster SP ON SP.ShippingName=SM.ShippingName
		                            LEFT JOIN dbo.PortMaster PM ON PM.PortName=SM.PortName
		                            LEFT JOIN dbo.TerminalMaster TM ON TM.TerminalName=SM.TerminalName
		                            LEFT JOIN dbo.CountryMaster C ON C.CountryName = SM.CountryName
                            ) AS source
                            ON target.ShipName = source.ShipName

                            WHEN MATCHED THEN
                                UPDATE SET         
                                    target.ShipTypeId = source.ShipTypeId,
		                            target.ShipUseId = source.ShipUseId,
		                            target.ShippingId = source.ShippingId,
		                            target.PortId = source.PortId,
		                            target.TerminalId = source.TerminalId,
		                            target.CountryId = source.CountryId,
		                            target.DepDate = source.DepDate,
		                            target.ArrDate = source.ArrDate,
		                            target.Freight = source.Freight, 
		                            target.LCapacity = source.LCapacity, 
                                    target.UpdatedOn = GETDATE(),
                                    target.UpdatedBy = {systemEmpId}
                            WHEN NOT MATCHED THEN
                                INSERT (ShipName,ShipTypeId,ShippingId,PortId,TerminalId,CountryId,ShipUseId,DepDate,ArrDate,Freight,LCapacity,CreatedBy, CreatedOn)
                                VALUES (source.ShipName,source.ShipTypeId,source.ShippingId,source.PortId,source.TerminalId,source.CountryId,source.ShipUseId
	                            ,source.DepDate,source.ArrDate,source.Freight,source.LCapacity,{systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion 
        #region For Makers Master 
        public async Task ClearMakerStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.Makers_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertMakerAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.Makers_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            }; 
            bulk.ColumnMappings.Add("CountryName", "CountryName");  
            bulk.ColumnMappings.Add("MakerName", "MakerName");  
            bulk.ColumnMappings.Add("Title", "Title");  
            bulk.ColumnMappings.Add("Keyword", "Keyword");  
            bulk.ColumnMappings.Add("Description", "Description");  
            bulk.ColumnMappings.Add("Canonical", "Canonical");  
            bulk.ColumnMappings.Add("Details", "Details");  
            bulk.ColumnMappings.Add("Logo", "Logo");  
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergeMakerAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"MERGE dbo.Makers AS target
                        USING (
                            SELECT   
		                        M.MakerName,
		                        M.Title,
		                        M.keyword,
		                        M.Description,
		                        M.Canonical,
		                        M.Details,
		                        M.Logo,
		                        C.ID AS CountryId
		                        FROM staging.Makers_Staging M	
		                        LEFT JOIN dbo.CountryMaster C ON C.CountryName = M.CountryName
                        ) AS source
                        ON target.MakerName = source.MakerName

                        WHEN MATCHED THEN
                            UPDATE SET
		                        target.MakerName=source.MakerName,
		                        target.Title=source.Title,
		                        target.keyword=source.keyword,
		                        target.Description=source.Description,
		                        target.Canonical=source.Canonical,
		                        target.Details=source.Details,
		                        target.Logo=source.Logo,
		                        target.CountryId = source.CountryId,
                                target.UpdatedOn = GETDATE(),
                                target.UpdatedBy = {systemEmpId}
                        WHEN NOT MATCHED THEN
                            INSERT (MakerName,Title,keyword,Description,Canonical,Details,Logo,CountryId,CreatedBy, CreatedOn)
                            VALUES (source.MakerName,source.Title,source.keyword,source.Description,source.Canonical,source.Details
	                        ,source.Logo,source.CountryId,{systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion
        #region For Models Master 
        public async Task ClearModelStagingAsync()
        {
            var query = "TRUNCATE TABLE staging.Models_Staging";
            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        public async Task BulkInsertModelAsync(DataTable dataTable)
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            using var bulk = new SqlBulkCopy(con)
            {
                DestinationTableName = "staging.Models_Staging",
                BatchSize = 1000,
                BulkCopyTimeout = 0
            }; 
            bulk.ColumnMappings.Add("CategoryName", "CategoryName");  
            bulk.ColumnMappings.Add("MakerName", "MakerName");  
            bulk.ColumnMappings.Add("ModelName", "ModelName");  
            bulk.ColumnMappings.Add("Title", "Title");  
            bulk.ColumnMappings.Add("Keyword", "Keyword");  
            bulk.ColumnMappings.Add("Description", "Description");   
            await bulk.WriteToServerAsync(dataTable);
        }
        public async Task MergeModelAsync()
        {
            var systemEmpId = _config.GetValue<int>("SyncSettings:SyncServiceEmpId");

            var query = $@"MERGE dbo.Models AS target
                        USING (
                            SELECT     
		                        M.ModelName,
		                        M.Title,
		                        M.keyword,
		                        M.Description,	
		                        C.ID AS CategoryId,
		                        MK.ID AS MakerId
		                        FROM staging.Models_Staging M	
		                        LEFT JOIN dbo.Category C ON C.CategoryName = M.CategoryName
		                        LEFT JOIN dbo.Makers MK ON MK.MakerName = M.MakerName
                        ) AS source
                        ON target.ModelName = source.ModelName

                        WHEN MATCHED THEN
                            UPDATE SET
		                        target.CategoryId=source.CategoryId,
		                        target.MakerId=source.MakerId,
		                        target.Title=source.Title,
		                        target.keyword=source.keyword,
		                        target.Description=source.Description,
                                target.UpdatedOn = GETDATE(),
                                target.UpdatedBy = {systemEmpId}
                        WHEN NOT MATCHED THEN
                            INSERT (CategoryId,MakerId,ModelName,Title,keyword,Description,CreatedBy, CreatedOn)
                            VALUES (source.CategoryId,source.MakerId,source.ModelName,source.Title,source.keyword,source.Description,{systemEmpId}, GETDATE());";

            await DbHelper.ExecuteAsync(_connectionString, query);
        }
        #endregion 
    }
}
