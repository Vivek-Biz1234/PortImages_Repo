using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class PortRepository : IPortRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<PortRepository> _logger;
        public PortRepository(IDapperRepository dapper, ILogger<PortRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddPortAsync(PortRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();     
                param.Add("@CountryId", request.CountryId);
                param.Add("@PortName", request.PortName); 
                param.Add("@SName", request.SName); 
                param.Add("@Email", request.Email); 
                param.Add("@Contact", request.Contact); 
                param.Add("@Fax", request.Fax); 
                param.Add("@Address", request.Address); 
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_port", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99               
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Port added successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    _ => new ApiResponse<object>(4, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "AddTerminal failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> UpdatePortAsync(PortRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@CountryId", request.CountryId);
                param.Add("@PortName", request.PortName);
                param.Add("@SName", request.SName);
                param.Add("@Email", request.Email);
                param.Add("@Contact", request.Contact);
                param.Add("@Fax", request.Fax);
                param.Add("@Address", request.Address);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_port", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;             
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Port updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Port not found !!"),
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
        public async Task<ApiResponse<object>> DeletePortAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_port", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Port deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Port not found !!"),
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
        public async Task<ApiResponse<PortRequestDTO?>> GetPortByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<PortRequestDTO>("dbo.usp_get_port_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<PortRequestDTO?>(-1, "Port not found !!", null);
                }
                return new ApiResponse<PortRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<PortRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<List<PortResponseDTO>>> GetPortListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<PortResponseDTO>("dbo.usp_get_port_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<PortResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<PortResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
