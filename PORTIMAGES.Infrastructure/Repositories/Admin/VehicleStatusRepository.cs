using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<VehicleStatusRepository> _logger;
        public VehicleStatusRepository(IDapperRepository dapper, ILogger<VehicleStatusRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddVehicleStatusAsync(VehicleStatusRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusName", request.StatusName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_Vehiclestatus", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "VehicleStatus added successfully !!"),
                    2 => new ApiResponse<object>(2, "VehicleStatus already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddVehicleStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> UpdateVehicleStatusAsync(VehicleStatusRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@StatusName", request.StatusName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_Vehiclestatus", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "VehicleStatus updated successfully !!"),
                    2 => new ApiResponse<object>(2, "VehicleStatus already exists !!"),
                    -1 => new ApiResponse<object>(-1, "VehicleStatus not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateVehicleStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> DeleteVehicleStatusAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_Vehiclestatus", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "VehicleStatus deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "VehicleStatus not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteVehicleStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<VehicleStatusRequestDTO>> GetVehicleStatusByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<VehicleStatusRequestDTO>("dbo.usp_get_Vehiclestatus_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<VehicleStatusRequestDTO>(-1, "Vehicle not found !!", null);
                }
                return new ApiResponse<VehicleStatusRequestDTO>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetVehicleStatusById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<VehicleStatusRequestDTO>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<List<VehicleStatusResponseDTO>>> GetVehicleStatusListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<VehicleStatusResponseDTO>("dbo.usp_get_Vehiclestatus_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<VehicleStatusResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetVehicleStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<VehicleStatusResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
