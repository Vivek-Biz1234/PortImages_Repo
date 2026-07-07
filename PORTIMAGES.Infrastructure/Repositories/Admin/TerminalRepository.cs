using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class TerminalRepository : ITerminalRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<TerminalRepository> _logger;
        public TerminalRepository(IDapperRepository dapper, ILogger<TerminalRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddTerminalAsync(TerminalRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PortId", request.PortId);
                param.Add("@TerminalName", request.TerminalName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_terminal", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Terminal added successfully !!"),
                    2 => new ApiResponse<object>(2, "Terminal already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "AddTerminal failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> UpdateTerminalAsync(TerminalRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@PortId", request.PortId);
                param.Add("@TerminalName", request.TerminalName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_terminal", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Terminal updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Terminal already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Terminal not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "UpdateTerminal failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> DeleteTerminalAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_terminal", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Terminal deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Terminal not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "DeleteTerminal failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }

        }
        public async Task<ApiResponse<TerminalRequestDTO?>> GetTerminalByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<TerminalRequestDTO>("dbo.usp_get_terminal_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<TerminalRequestDTO?>(-1, "Terminal not found !!", null);
                }
                return new ApiResponse<TerminalRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<TerminalRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<List<TerminalResponseDTO>>> GetTerminalListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<TerminalResponseDTO>("dbo.usp_get_terminal_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<TerminalResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<TerminalResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
