using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class INSOrganizationRepository : IINSOrganizationRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<INSOrganizationRepository> _logger;

        public INSOrganizationRepository(IDapperRepository dapper, ILogger<INSOrganizationRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddINSOrganizationAsync(INSOrganizationRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@OrganizationName", request.OrganizationName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_OrganizationNameStatus", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "OrganizationName added successfully !!"),
                    2 => new ApiResponse<object>(2, "OrganizationName already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddInventoryStatus failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<INSOrganizationRequestDTO>> GetINSOrganizationStatusByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<INSOrganizationRequestDTO>("dbo.usp_get_OrganizationName_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<INSOrganizationRequestDTO>(-1, "OrganizationName not found !!", null);
                }
                return new ApiResponse<INSOrganizationRequestDTO>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetInventoryStatusById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<INSOrganizationRequestDTO>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<List<INSOrganizationResponseDTO>>> GetINSOrganizationListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<INSOrganizationResponseDTO>("dbo.usp_get_INSOrganization_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<INSOrganizationResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetInventoryStatusList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<INSOrganizationResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateINSOrganizationAsync(INSOrganizationRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@OrganizationName", request.OrganizationName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_INSOrganization", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "OrganizationName updated successfully !!"),
                    2 => new ApiResponse<object>(2, "OrganizationName already exists !!"),
                    -1 => new ApiResponse<object>(-1, "OrganizationName not found !!"),
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

        public async Task<ApiResponse<object>> DeleteINSOrganizationAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_INS_Organization", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "INSOrganization deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "INSOrganization not found !!"),
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
    }
}
