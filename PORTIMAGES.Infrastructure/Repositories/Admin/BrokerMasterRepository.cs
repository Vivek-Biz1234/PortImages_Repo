using Dapper;
using Microsoft.Extensions.Logging; 
using PORTIMAGES.Application.BrokerConsignee.DTOs; 
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;
using PORTIMAGES.Common.Extensions;
using PORTIMAGES.Application.BrokerConsignee.Interfaces;
namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class BrokerMasterRepository: IBrokerMasterRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<BrokerMasterRepository> _logger;
        public BrokerMasterRepository(IDapperRepository dapper, ILogger<BrokerMasterRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddBrokerAsync(BrokerMasterRequestDTO request)
        {
            try
            { 
                var param = new DynamicParameters();
                param.Add("@FullName", request.FullName);
                param.Add("@Email", request.Email);
                param.Add("@Mobile", request.Mobile);
                param.Add("@Address", request.Address);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_brokers", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage"); 
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Broker added successfully!"),
                    2 => new ApiResponse<object>(2, "Email id already exists!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "AddBroker");
            }
        }
        public async Task<ApiResponse<object>> UpdateBrokerAsync(BrokerMasterRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@FullName", request.FullName);
                param.Add("@Email", request.Email);
                param.Add("@Mobile", request.Mobile);
                param.Add("@Address", request.Address);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_brokers", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");
                return result switch
                {
                    -1 => new ApiResponse<object>(-1, "Broker not found !"),
                    1 => new ApiResponse<object>(1, "Broker updated successfully!"),
                    2 => new ApiResponse<object>(2, "Email id already exists!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "UpdateBroker");
            }
        }

        public async Task<ApiResponse<object>> DeleteBrokerAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_brokers", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");
                return result switch
                {
                    -1 => new ApiResponse<object>(-1, "Broker not found !"),
                    1 => new ApiResponse<object>(1, "Broker deleted successfully!"), 
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "DeleteBroker");
            }
        }
        public async Task<ApiResponse<BrokerMasterRequestDTO?>> GetBrokerByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<BrokerMasterRequestDTO?>("dbo.usp_get_brokers_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<BrokerMasterRequestDTO?>(-1, "Broker not found !!", null);
                }
                return new ApiResponse<BrokerMasterRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<BrokerMasterRequestDTO?>(ex, _logger, "GetBrokerById");
            }

        }
        public async Task<ApiResponse<List<BrokerMasterResponseDTO>>> GetBrokerListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<BrokerMasterResponseDTO>("dbo.usp_get_Broker_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<BrokerMasterResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<List<BrokerMasterResponseDTO>>(ex, _logger, "GetBrokerList");
            }

        }
    }
}
