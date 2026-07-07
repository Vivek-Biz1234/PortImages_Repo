using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class INSDestinationRepository : IINSDestinationRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<INSDestinationRepository> _logger;
        public INSDestinationRepository(IDapperRepository dapper, ILogger<INSDestinationRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }

        public async Task<ApiResponse<object>> AddINSDestinationAsync(INSDestinationRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@DestinationName", request.DestinationName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_INSDestionationMaster", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status")??-99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "INSDestination added successfully !!"),
                    2 => new ApiResponse<object>(2, "INSDestination already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddINSDestination failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateINSDestinationAsync(INSDestinationRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@DestinationName", request.DestinationName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_INSDestination", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "INSDestination updated successfully !!"),
                    2 => new ApiResponse<object>(2, "INSDestination already exists !!"),
                    -1 => new ApiResponse<object>(-1, "INSDestination not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateINSDestination failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> DeleteInventoryStatusAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_INSDestinationstatus", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "INSDestination deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "INSDestination not found !!"),
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

        public async Task<ApiResponse<INSDestinationRequestDTO>> GetINSDestinationByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<INSDestinationRequestDTO>("dbo.usp_get_INSDestination_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<INSDestinationRequestDTO>(-1, "Destination not found !!", null);
                }
                return new ApiResponse<INSDestinationRequestDTO>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetINSDestinationById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<INSDestinationRequestDTO>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<List<INSDestinationResponseDTO>>> GetINSDestionationListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<INSDestinationResponseDTO>("dbo.usp_get_INSDestionation_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<INSDestinationResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetINSDestionationList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<INSDestinationResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

       
    }
}
