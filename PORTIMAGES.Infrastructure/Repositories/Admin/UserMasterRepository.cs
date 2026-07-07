using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class UserMasterRepository : IUserMasterRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<UserMasterRepository> _logger;
        public UserMasterRepository(IDapperRepository dapper, ILogger<UserMasterRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
        public async Task<ApiResponse<object>> AddCompanyAsync(UserRequestDTO request)
        {
            try
            {
                string passwordHash = CryptoHelper.Encrypt(request.Password ?? request.Contact);
                var param = new DynamicParameters();
                param.Add("@UserName", request.UserName);
                param.Add("@Email", request.Email);
                param.Add("@Contact", request.Contact);
                param.Add("@PasswordHash", passwordHash);
                param.Add("@ContactPerson", request.ContactPerson);
                param.Add("@Address", request.Address);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_user", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;//1,2,-99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "User added successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    _ => new ApiResponse<object>(4, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddCompany failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> UpdateCompanyAsync(UserRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@UserName", request.UserName);
                param.Add("@Email", request.Email);
                param.Add("@Contact", request.Contact);
                param.Add("@ContactPerson", request.ContactPerson);
                param.Add("@Address", request.Address);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_user", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "User updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    -1 => new ApiResponse<object>(-1, "User not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateCompany failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<object>> DeleteCompanyAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_user", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "User deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "User not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteCompany failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
        public async Task<ApiResponse<UserRequestDTO?>> GetCompanyByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<UserRequestDTO>("dbo.usp_get_user_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<UserRequestDTO?>(-1, "User not found !!", null);
                }
                return new ApiResponse<UserRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetCompanyById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<UserRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }

        }
        public async Task<ApiResponse<List<UserResponseDTO>>> GetCompanyListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<UserResponseDTO>("dbo.usp_get_user_list", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<UserResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetCompanyList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<UserResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
