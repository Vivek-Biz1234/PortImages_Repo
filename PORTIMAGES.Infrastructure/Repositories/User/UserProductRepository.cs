using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Application.User.Extensions;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.User
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<UserProductRepository> _logger;

        public UserProductRepository(IDapperRepository dapper, ILogger<UserProductRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }


        public async Task<ApiResponse<List<MyProductResponseDTOs>>> GetProductListAsync(MyProductRequestDTOs request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ClientId", request.ClientId);
                param.Add("@TerminalID", request.TerminalID);
                param.Add("@InsOrganizationId", request.InsOrganizationID);
                param.Add("@InsDestinationId", request.InsDestinationId);
                param.Add("@InventoryStatusId", request.InventoryStatusId);
                param.Add("@ModelId", request.ModelId);
                param.Add("@VehicleStatusId", request.VehicleStatusId);
                param.Add("@InsStatusId", request.InsStatusId);
                param.Add("@ShipId", request.ShipId);
                param.Add("@YardInDate", request.YardInDate);
                param.Add("@YardOutDate", request.YardOutDate);
                param.Add("@InsDateFrom", request.InsDateFrom);
                param.Add("@InsDateTo", request.InsDateTo);
                param.Add("@ChassisNo", request.ChassisNo);
                param.Add("@VoyageNo", request.VoyageNo);
                param.Add("@ContainerNo", request.ContainerNo);

                var data = await _dapper.QueryAsync<MyProductResponseDTOs>("dbo.usp_get_myproduct_list", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<MyProductResponseDTOs>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                return new ApiResponse<List<MyProductResponseDTOs>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<ViewProductDetailsDTO>> GetProductDetailsByIdAsync(long id, long clientId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", id);
                param.Add("@ClientId", clientId);

                using var multi = await _dapper.QueryMultipleAsync("dbo.usp_get_product_details_with_images", param, CommandType.StoredProcedure);

                var product = await multi.ReadFirstOrDefaultAsync<ViewProductDetailsDTO>();
                if (product == null)
                {
                    return new ApiResponse<ViewProductDetailsDTO>(-1, "Product not found !!", null);
                } 
                var images = (await multi.ReadAsync<ProductImageDTO>()).Select(x => x.ImagePath).ToList();
                product.Images = images; 

                return new ApiResponse<ViewProductDetailsDTO>(1, "Success", product);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetProductDetails failed | ErrorId: {ErrorId}", errorId); 
                return new ApiResponse<ViewProductDetailsDTO>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId, null);
            }
        }

    }
}
