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
    public class ConsigneeMasterRepository: IConsigneeMasterRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ConsigneeMasterRepository> _logger;
        public ConsigneeMasterRepository(IDapperRepository dapper, ILogger<ConsigneeMasterRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddConsigneeAsync(ConsigneeMasterRequestDTO request)
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
                await _dapper.ExecuteAsync("dbo.usp_add_consignee", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage"); 
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Consignee added successfully!"),
                    2 => new ApiResponse<object>(2, "Email id already exists!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "AddConsignee");
            }
        }
        public async Task<ApiResponse<object>> UpdateConsigneeAsync(ConsigneeMasterRequestDTO request)
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
                await _dapper.ExecuteAsync("dbo.usp_update_consignee", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");
                return result switch
                {
                    -1 => new ApiResponse<object>(-1, "Consignee not found !"),
                    1 => new ApiResponse<object>(1, "Consignee updated successfully!"),
                    2 => new ApiResponse<object>(2, "Email id already exists!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "UpdateConsignee");
            }
        }

        public async Task<ApiResponse<object>> DeleteConsigneeAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_consignee", param, CommandType.StoredProcedure);
                var result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");
                return result switch
                {
                    -1 => new ApiResponse<object>(-1, "Consignee not found !"),
                    1 => new ApiResponse<object>(1, "Consignee deleted successfully!"), 
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "DeleteConsignee");
            }
        }
        public async Task<ApiResponse<ConsigneeMasterRequestDTO?>> GetConsigneeByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ConsigneeMasterRequestDTO?>("dbo.usp_get_consignee_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<ConsigneeMasterRequestDTO?>(-1, "Consignee not found !!", null);
                }
                return new ApiResponse<ConsigneeMasterRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<ConsigneeMasterRequestDTO?>(ex, _logger, "GetConsigneeById");
            }

        }
        public async Task<ApiResponse<List<ConsigneeMasterResponseDTO>>> GetConsigneeListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ConsigneeMasterResponseDTO>("dbo.usp_get_consignee_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<ConsigneeMasterResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<List<ConsigneeMasterResponseDTO>>(ex, _logger, "GetConsigneeList");
            }

        }
    }
}
