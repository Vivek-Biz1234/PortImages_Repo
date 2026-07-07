using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;


namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ShipUseRepository : IShipUseRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ShipUseRepository> _logger;

        public ShipUseRepository(IDapperRepository dapper, ILogger<ShipUseRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> AddShipUseAsync(ShipUseRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UseType", request.UseType);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_ShipUse", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "UseType added successfully !!"),
                    2 => new ApiResponse<object>(2, "UseType already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddUseType failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> DeleteShipUseStatusAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_ShipUse", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "ShipUse deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "ShipUse not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteShipUseStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<ShipUseRequestDTO>> GetShipUseStatusByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ShipUseRequestDTO>("dbo.usp_get_UseType_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<ShipUseRequestDTO>(-1, "ShipUse not found !!", null);
                }
                return new ApiResponse<ShipUseRequestDTO>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetInventoryStatusById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<ShipUseRequestDTO>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<List<ShipUseResponseDTO>>> GetShipUseStatusListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ShipUseResponseDTO>("dbo.usp_get_UseType_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<ShipUseResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetShipUse failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<ShipUseResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateShipUseStatusStatusAsync(ShipUseRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@UseType", request.UseType);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_ShipUse", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "UseType updated successfully !!"),
                    2 => new ApiResponse<object>(2, "UseType already exists !!"),
                    -1 => new ApiResponse<object>(-1, "UseType not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateInventoryStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
