using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;
using PORTIMAGES.Common.Extensions; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ProductAdditionalInfoRepository: IProductAdditionalInfoRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ProductAdditionalInfoRepository> _logger;
        public ProductAdditionalInfoRepository(IDapperRepository dapper, ILogger<ProductAdditionalInfoRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }
        public async Task<ApiResponse<List<FoundCarResponseDTO>>> GetFoundCarsAsync(FoundCarRequestDTO req)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ChassisNo", req.ChassisNo);
                param.Add("@ShipID", req.ShipId);
                param.Add("@TerminalId", req.TerminalId);
                param.Add("@CarStatus", req.CarStatus);
                param.Add("@PageIndex", req.PageIndex);
                param.Add("@PageSize", req.PageSize);
                param.Add("@Status", dbType: DbType.Int16,direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                var data = await _dapper.QueryAsync<FoundCarResponseDTO>("dbo.usp_get_found_cars", param, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return new ApiResponse<List<FoundCarResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); 
                return new ApiResponse<List<FoundCarResponseDTO>>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            } 
        }
        public async Task<ApiResponse<object>> AssignTerminalAsync(CommonAssignmentRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", request.ProductId);
                param.Add("@TerminalId", request.ReferenceId); 
                param.Add("@CreatedBy", request.CreatedBy); 
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_assign_prod_terminal", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Terminal assigned successfully !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); 
                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }

        public async Task<ApiResponse<object>> MarkProductFoundOrNotFoundAsync(CommonAssignmentRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", request.ProductId); 
                param.Add("@IsFound", request.ReferenceId); 
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_mark_prod_found_notfound", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Product mark as "+ (request.ReferenceId == 1?"found":"not found") +"!!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
    }
}
