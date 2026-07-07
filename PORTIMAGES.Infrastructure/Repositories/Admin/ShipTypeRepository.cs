using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;
 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ShipTypeRepository : IShipTypeRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ShipTypeRepository> _logger;

        public ShipTypeRepository(IDapperRepository dapper, ILogger<ShipTypeRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }

        public async Task<ApiResponse<object>> AddShipTypeAsync(ShipTypeRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@TypeName", request.TypeName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_TypeName", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "TypeName added successfully !!"),
                    2 => new ApiResponse<object>(2, "TypeName already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "TypeName failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> DeleteShipTypeStatusAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_ShipType", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "ShipType deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "ShipType not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteInventoryStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<List<ShipTypeResponseDTO>>> GetShipTypeListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ShipTypeResponseDTO>("dbo.usp_get_ShipTypestatus_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<ShipTypeResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetShipTypeStatusList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<ShipTypeResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<ShipTypeRequestDTO>> GetShipTypeStatusByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ShipTypeRequestDTO>("dbo.usp_get_ShipType_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<ShipTypeRequestDTO>(-1, "ShipType not found !!", null);
                }
                return new ApiResponse<ShipTypeRequestDTO>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetShipTypeStatusById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<ShipTypeRequestDTO>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateShipTypeAsync(ShipTypeRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@TypeName", request.TypeName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_ShipType", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "TypeName updated successfully !!"),
                    2 => new ApiResponse<object>(2, "TypeName already exists !!"),
                    -1 => new ApiResponse<object>(-1, "TypeName not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateTypeName failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
