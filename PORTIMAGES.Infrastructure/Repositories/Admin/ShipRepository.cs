using Dapper;
using Microsoft.Extensions.Logging; 
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ShipRepository: IShipRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ShipRepository> _logger;
        public ShipRepository(IDapperRepository dapper, ILogger<ShipRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddShipAsync(ShipRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ShipName", request.ShipName);
                param.Add("@ShipTypeId", request.ShipTypeId);
                param.Add("@ShippingId", request.ShippingId);
                param.Add("@PortId", request.PortId);
                param.Add("@TerminalId", request.TerminalId);
                param.Add("@CountryId", request.CountryId);
                param.Add("@ShipUseId", request.ShipUseId);
                param.Add("@DepDate", request.DepDate);
                param.Add("@ArrDate", request.ArrDate);
                param.Add("@Freight", request.Freight);
                param.Add("@LCapacity", request.LCapacity);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_ship", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Ship added successfully !!"),
                    2 => new ApiResponse<object>(2, "Ship already exists !!"),
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
        public async Task<ApiResponse<object>> UpdateShipAsync(ShipRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID); 
                param.Add("@ShipName", request.ShipName);
                param.Add("@ShipTypeId", request.ShipTypeId);
                param.Add("@ShippingId", request.ShippingId);
                param.Add("@PortId", request.PortId);
                param.Add("@TerminalId", request.TerminalId);
                param.Add("@CountryId", request.CountryId);
                param.Add("@ShipUseId", request.ShipUseId);
                param.Add("@DepDate", request.DepDate);
                param.Add("@ArrDate", request.ArrDate);
                param.Add("@Freight", request.Freight);
                param.Add("@LCapacity", request.LCapacity);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_ship", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Ship updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Ship already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Ship not found !!"),
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
        public async Task<ApiResponse<object>> DeleteShipAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_ship", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Ship deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Ship not found !!"),
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
        public async Task<ApiResponse<ShipRequestDTO?>> GetShipByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ShipRequestDTO>("dbo.usp_get_ship_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<ShipRequestDTO?>(-1, "Terminal not found !!", null);
                }
                return new ApiResponse<ShipRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<ShipRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<List<ShipResponseDTO>>> GetShipListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ShipResponseDTO>("dbo.usp_get_ship_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<ShipResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetTerminalList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<ShipResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
