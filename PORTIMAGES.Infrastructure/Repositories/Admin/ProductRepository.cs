using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Extensions;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using PORTIMAGES.Common.Extensions;
using System.Data;
using PORTIMAGES.Common.Helpers; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IProductFilesRepository _productFilesRepository;

        public ProductRepository(IDapperRepository dapper, ILogger<ProductRepository> logger, IProductFilesRepository productFilesRepository)
        {
            _dapper = dapper;
            _logger = logger;
            _productFilesRepository = productFilesRepository;
        }

        #region ADD PRODUCT
        public async Task<ApiResponse<object>> AddProductAsync(AddProductRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@ChassisNo", request.ChassisNo);
                param.Add("@InvoicePrice", request.InvoicePrice);
                param.Add("@ClientId", request.ClientId);
                param.Add("@ShipId", request.ShipId);
                param.Add("@ModelId", request.ModelId);
                param.Add("@InventoryStatusId", request.InventoryStatusId);
                param.Add("@VehicleStatusId", request.VehicleStatusId);

                // INS
                param.Add("@InsOrganizationId", request.InsOrganizationId);
                param.Add("@InsDestinationId", request.InsDestinationId);
                param.Add("@InsStatusId", request.InsStatusId);
                param.Add("@InsDate", request.InsDate);

                param.Add("@NFCNo", request.NFCNO);
                param.Add("@REFNo", request.REFNO);
                param.Add("@YardInNo", request.YardInNo);
                param.Add("@YardInPlace", request.YardInPlace);
                param.Add("@YardInDate", request.YardInDate);
                param.Add("@YardOutDate", request.YardOutDate);
                param.Add("@ScheduledShippingDate", request.ScheduledShippingDate);
                param.Add("@ShippingDate", request.ShippingDate);
                param.Add("@VoyageNo", request.VoyageNo);
                param.Add("@StoragePeriod", request.StoragePeriod);
                param.Add("@ContainerNo", request.ContainerNo);
                param.Add("@Mileage", request.Mileage);
                param.Add("@Location", request.Location);
                param.Add("@InnerCargo", request.InnerCargo);
                param.Add("@Notes", request.Notes);
                param.Add("@ReasonFailure", request.ReasonFailure);

                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@SourceId", request.SourceId);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_add_products", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Product added successfully !!"),
                    2 => new ApiResponse<object>(2, "Chassis number already exists !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region UPDATE PRODUCT
        public async Task<ApiResponse<object>> UpdateProductAsync(AddProductRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@ID", request.ID);
                param.Add("@ChassisNo", request.ChassisNo);
                param.Add("@ClientId", request.ClientId);
                param.Add("@ShipId", request.ShipId);
                param.Add("@ModelId", request.ModelId);
                param.Add("@InventoryStatusId", request.InventoryStatusId);
                param.Add("@VehicleStatusId", request.VehicleStatusId);

                // INS
                param.Add("@InsOrganizationId", request.InsOrganizationId);
                param.Add("@InsDestinationId", request.InsDestinationId);
                param.Add("@InsStatusId", request.InsStatusId);
                param.Add("@InsDate", request.InsDate);

                param.Add("@NFCNo", request.NFCNO);
                param.Add("@REFNo", request.REFNO);
                param.Add("@YardInNo", request.YardInNo);
                param.Add("@YardInPlace", request.YardInPlace);
                param.Add("@YardInDate", request.YardInDate);
                param.Add("@YardOutDate", request.YardOutDate);
                param.Add("@ScheduledShippingDate", request.ScheduledShippingDate);
                param.Add("@ShippingDate", request.ShippingDate);
                param.Add("@VoyageNo", request.VoyageNo);
                param.Add("@StoragePeriod", request.StoragePeriod);
                param.Add("@ContainerNo", request.ContainerNo);
                param.Add("@Mileage", request.Mileage);
                param.Add("@Location", request.Location);
                param.Add("@InnerCargo", request.InnerCargo);
                param.Add("@Notes", request.Notes);
                param.Add("@ReasonFailure", request.ReasonFailure);

                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@SourceId", request.SourceId);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_update_products", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Product updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Chassis number already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Product not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region DELETE PRODUCT
        public async Task<ApiResponse<object>> DeleteProductAsync(long id, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_products", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                if (result == 1)
                {
                    await DeleteAllDetailsOfProduct(id, deletedBy);
                }

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Product deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Product not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region GET BY ID
        public async Task<ApiResponse<AddProductRequestDTO?>> GetProductByIdAsync(long id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<AddProductRequestDTO>("dbo.usp_get_products_by_id", new { ID = id }, CommandType.StoredProcedure);

                if (data == null)
                {
                    return new ApiResponse<AddProductRequestDTO?>(-1, "Product not found !!", null);
                }

                return new ApiResponse<AddProductRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductById failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<AddProductRequestDTO?>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region GET LIST
        public async Task<ApiResponse<List<ProductResponseDTO>>> GetProductListAsync(ViewProductsRequestDTO req)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PageIndex", req.PageIndex);
                param.Add("@PageSize", req.PageSize);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                var data = await _dapper.QueryAsync<ProductResponseDTO>("dbo.usp_get_products_list", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");
                return new ApiResponse<List<ProductResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<ProductResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region Created By Vivek on 10-Apr-2026  
        private async Task DeleteAllDetailsOfProduct(long productId, int deletedBy)
        {
            try
            {
                var response = _productFilesRepository.GetProductImagesAsync(productId);
                var images = response.Result.Data;
                if (images.Count > 0)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        await _productFilesRepository.DeleteProductImageAsync(images[i].ImageId, deletedBy);
                    }
                }
                await _productFilesRepository.DeleteProductComponentDetailsAsync(productId, deletedBy);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
            }
        }
        #endregion

        #region Assign Consignee for product
        public async Task<ApiResponse<List<ProductConsigneeResponseDTO>>> GetProductConsigneeListAsync(ProductConsigneeRequestDTO req)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ChassisNo", req.ChassisNo);
                param.Add("@ClientId", req.ClientId);
                param.Add("@ModelId", req.ModelId);
                var data = await _dapper.QueryAsync<ProductConsigneeResponseDTO>("dbo.usp_get_cars_assign_consignee", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<ProductConsigneeResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<ProductConsigneeResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        public async Task<ApiResponse<List<ProductConsigneeResponseDTO>>> GetProductConsigneeByIdAsync(int ProductId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", ProductId);
                var data = await _dapper.QueryAsync<ProductConsigneeResponseDTO>("dbo.usp_get_cars_assign_consignee_byid", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<ProductConsigneeResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<ProductConsigneeResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        public async Task<ApiResponse<object>> AssignConsigneeAsync(AssignProdConsigneeRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", request.ProductId);
                param.Add("@ConsigneeName", request.ConsigneeName);
                param.Add("@ConsigneeEmail", request.ConsigneeEmail);
                param.Add("@ConsigneeMobile", request.ConsigneeMobile);
                param.Add("@ConsigneeAddress", request.ConsigneeAddress);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@SourceId", MasterSourceHelper.AdminPanel);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_assign_consignee", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Consignee assigned successfully !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        public async Task<ApiResponse<object>> AssignConsigneeAsync_Old(AssignProdConsigneeRequestDTO_Old request)
        {
            try
            {
                var param = new DynamicParameters();
                var table = DataTableHelper.ToDataTable(request.ProductIds);
                param.Add("@ProductIds", table.AsTableValuedParameter("dbo.ProductConsigneeTableType"));
                param.Add("@ConsigneeId", request.ConsigneeId);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_assign_consignee", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Consignee assigned successfully !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region Assign Broker for product
        public async Task<ApiResponse<List<ProductBrokerResponseDTO>>> GetProductBrokerListAsync(ProductBrokerRequestDTO req)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ChassisNo", req.ChassisNo);
                param.Add("@ClientId", req.ClientId);
                param.Add("@ModelId", req.ModelId);
                var data = await _dapper.QueryAsync<ProductBrokerResponseDTO>("dbo.usp_get_cars_assign_broker", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<ProductBrokerResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<ProductBrokerResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        public async Task<ApiResponse<object>> AssignBrokerAsync(AssignProdBrokerRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                var table = DataTableHelper.ToDataTable(request.ProductIds);
                param.Add("@ProductIds", table.AsTableValuedParameter("dbo.ProductConsigneeTableType"));
                param.Add("@BrokerId", request.BrokerId);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_assign_broker", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Broker assigned successfully !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region ADD PRODUCT VIA FILE
        public async Task<ApiResponse<object>> ImportProductViaFileAsync(DataTable dt)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@VehicleData", dt.AsTableValuedParameter("dbo.VehicleImportType")); 
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_import_products", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Product added successfully !!"), 
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        public async Task<List<string>> GetExistingChassisViaFileAsync(DataTable dt)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@VehicleData", dt.AsTableValuedParameter("dbo.VehicleImportType"));
                var result = await _dapper.QueryAsync<string>("dbo.usp_validate_import_products",param,commandType: CommandType.StoredProcedure);
                return result.ToList(); 
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddProduct failed | ErrorId: {ErrorId}", errorId);
                return new List<string>();
            }
        }
        #endregion
    }
}
