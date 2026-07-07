using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ProductFilesRepository : IProductFilesRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ProductFilesRepository> _logger;
        private readonly FileHelper _fileHelper;
        string allowedExtensions = ".jpg,.png,.jpeg,.webp";



        public ProductFilesRepository(IDapperRepository dapper, ILogger<ProductFilesRepository> logger, FileHelper fileHelper)
        {
            _dapper = dapper;
            _logger = logger;
            _fileHelper = fileHelper;
        }


        #region UploadImage 
        public async Task<ApiResponse<object>> AddProductImageAsync(UploadProductImageRequesDTO request)
        {
            try
            {                
                int successCount = 0;
                int failCount = 0;
                int componentStatus = 0;
                if (!string.IsNullOrEmpty(request.ComponentName) && request.ComponentWeight > 0 && !string.IsNullOrEmpty(request.WeightUnit))
                {
                    var param = new DynamicParameters();
                    param.Add("@ProductId", request.ProductId); 
                    param.Add("@ComponentName", request.ComponentName); 
                    param.Add("@ComponentWeight", request.ComponentWeight); 
                    param.Add("@WeightUnit", request.WeightUnit); 
                    param.Add("@CreatedBy", request.CreatedBy);
                    param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                    param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                    await _dapper.ExecuteAsync("dbo.usp_add_product_inner_details", param, CommandType.StoredProcedure);

                    var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);
                    componentStatus = (int)status;
                }
                if ((request.Pimages == null || !request.Pimages.Any()) && componentStatus>0)
                {
                    return new ApiResponse<object>((short)ResultStatus.Success, "Component details updated", null);
                }
                else if ((request.Pimages == null || !request.Pimages.Any()) && componentStatus != 1)
                {
                    return new ApiResponse<object>((short)ResultStatus.Failed, "No images selected for upload", null);
                }
                foreach (var image in request.Pimages)
                {
                    string? imgPath = null;

                    try
                    {
                        imgPath = await _fileHelper.SaveFileAsync(image, "ProductImage", allowedExtensions, 1);

                        var param = new DynamicParameters();
                        param.Add("@ProductId", request.ProductId);
                        param.Add("@ImgPath", imgPath);
                        param.Add("@CreatedBy", request.CreatedBy);
                        param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                        param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                        await _dapper.ExecuteAsync("USP_UploadProductImage", param, CommandType.StoredProcedure);

                        var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);

                        if (status == ResultStatus.Success)
                        {
                            successCount++;
                        }
                        else
                        {
                            failCount++;
                            if (!string.IsNullOrEmpty(imgPath))
                                _fileHelper.DeleteFile(imgPath);
                        }
                    }
                    catch
                    {
                        failCount++;
                        if (!string.IsNullOrEmpty(imgPath))
                            _fileHelper.DeleteFile(imgPath);
                    }
                }
                if (successCount > 0 && failCount == 0)
                {
                    return ApiResponseMapper.Map(ResultStatus.Success, $"{successCount} image(s)", CrudAction.Added);
                }

                if (successCount > 0 && failCount > 0)
                {
                    return new ApiResponse<object>((short)ResultStatus.Success, $"{successCount} image(s) uploaded, {failCount} failed.", null);
                }

                return ApiResponseMapper.Map(
                    ResultStatus.Failed,
                    "Product images",
                    CrudAction.Added
                );
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "AddProductImage");
            }
        }

        public async Task<ApiResponse<List<ProductImageResponseDTO>>> GetProductImagesAsync(long productId)
        {
            try
            {
                var data = await _dapper.QueryAsync<ProductImageResponseDTO>("dbo.usp_get_product_images", new { ProductId = productId }, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<ProductImageResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<List<ProductImageResponseDTO>>(ex, _logger, "GetProductImages");
            }
        }
        public async Task<ApiResponse<object>> DeleteProductImageAsync(long id, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@ImagePath", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_delete_product_image", param, CommandType.StoredProcedure);

                var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);

                if (status != ResultStatus.Success)
                    return ApiResponseMapper.Map(status, "Product Image", CrudAction.Deleted);

                var imagePath = param.Get<string>("@ImagePath");

                if (!string.IsNullOrWhiteSpace(imagePath))
                {
                    _fileHelper.DeleteFile(imagePath);
                }

                return ApiResponseMapper.Map(ResultStatus.Success, "Product Image", CrudAction.Deleted);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "DeleteProductImage");
            }
        }

        public async Task<ApiResponse<List<ProductInnerDetailsDTO>>> GetProductInnerDetailsAsync(long productId)
        {
            try
            {
                var data = await _dapper.QueryAsync<ProductInnerDetailsDTO>("dbo.usp_get_product_inner_Details", new { ProductId = productId }, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<ProductInnerDetailsDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<List<ProductInnerDetailsDTO>>(ex, _logger, "GetProductImages");
            }
        }

        #endregion

        #region Created By Vivek on 10-Apr-2026 
        public async Task<ApiResponse<object>> DeleteProductComponentDetailsAsync(long productID, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductID", productID);
                param.Add("@DeletedBy", deletedBy);                
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_delete_product_component_details", param, CommandType.StoredProcedure);

                var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);

                if (status != ResultStatus.Success)
                    return ApiResponseMapper.Map(status, "Product Component Details", CrudAction.Deleted); 

                return ApiResponseMapper.Map(ResultStatus.Success, "Product Component Details", CrudAction.Deleted);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "DeleteProductComponentDetailsAsync");
            }
        }
        #endregion
    }
}
