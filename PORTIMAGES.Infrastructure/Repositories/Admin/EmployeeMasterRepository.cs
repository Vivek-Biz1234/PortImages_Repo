using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Extensions;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class EmployeeMasterRepository : IEmployeeMasterRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<EmployeeMasterRepository> _logger;
        private readonly FileHelper _fileHelper;
        string allowedExtensions = ".jpg,.png,.jpeg,.webp";
        public EmployeeMasterRepository(IDapperRepository dapper, ILogger<EmployeeMasterRepository> logger, FileHelper fileHelper)
        {
            this._dapper = dapper;
            this._logger = logger;
            this._fileHelper = fileHelper;
        }
        public async Task<ApiResponse<object>> AddEmployeeAsync(EmployeeMasterRequestDTO request)
        {
            try
            {
                string passwordHash = CryptoHelper.Encrypt(request.Mobile!);
                var param = new DynamicParameters();
                param.Add("@FullName", request.FullName);
                param.Add("@Email", request.Email);
                param.Add("@Mobile", request.Mobile);
                param.Add("@PasswordHash", passwordHash);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@ProfileImage", request.Profile);
                param.Add("@DOB", request.DOB);
                param.Add("@DesignationId", request.DesignationId);
                param.Add("@RoleId", request.RoleId);
                param.Add("@Address", request.Address);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_employee", param, CommandType.StoredProcedure);
                var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);
                string sqlError = param.Get<string>("@ErrorMessage");
                return ApiResponseMapper.Map(status, "Employee", CrudAction.Added);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "AddEmployee");
            }
        }
        public async Task<ApiResponse<object>> UpdateEmployeeAsync(EmployeeMasterRequestDTO request)
        {
            try
            {
                string? imgPath = null, oldImagePath = null;
                if (request.ProfilePicture != null)
                {
                    var res = await GetEmployeeByIdAsync(request.ID);
                    oldImagePath = res.Data.Profile;
                    if (!string.IsNullOrEmpty(oldImagePath))
                    {
                        _fileHelper.DeleteFile(oldImagePath);
                    }
                    imgPath = await _fileHelper.SaveFileAsync(request.ProfilePicture, "EmpProfileImages", allowedExtensions, 1);
                }
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@FullName", request.FullName);
                param.Add("@Email", request.Email);
                param.Add("@Mobile", request.Mobile);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@ProfileImage", imgPath);
                param.Add("@DOB", request.DOB);
                param.Add("@DesignationId", request.DesignationId);
                param.Add("@RoleId", request.RoleId);
                param.Add("@Address", request.Address);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_update_employee", param, CommandType.StoredProcedure);
                var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);
                string sqlError = param.Get<string>("@ErrorMessage");
                return ApiResponseMapper.Map(status, "Employee", CrudAction.Updated);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "UpdateEmployee");
            }
        }

        public async Task<ApiResponse<object>> DeleteEmployeeAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_delete_employee", param, CommandType.StoredProcedure);
                var status = (ResultStatus)(param.Get<short?>("@Status") ?? -99);
                return ApiResponseMapper.Map(status, "Employee", CrudAction.Deleted);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "DeleteEmployee");
            }
        }
        public async Task<ApiResponse<EmployeeMasterRequestDTO?>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<EmployeeMasterRequestDTO?>("dbo.usp_get_employee_by_id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<EmployeeMasterRequestDTO?>(-1, "Employee not found !!", null);
                }
                return new ApiResponse<EmployeeMasterRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<EmployeeMasterRequestDTO?>(ex, _logger, "DeleteEmployee");
            }

        }
        public async Task<ApiResponse<List<EmployeeMasterResponseDTO>>> GetEmployeeListAsync(int _id = 0)
        {
            try
            {
                var data = await _dapper.QueryAsync<EmployeeMasterResponseDTO>("dbo.usp_get_employee_list", new { ID = _id }, CommandType.StoredProcedure);
                var list = data.ToList();
                list.EncryptIds();
                return new ApiResponse<List<EmployeeMasterResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                return ApiExceptionHandler.Handle<List<EmployeeMasterResponseDTO>>(ex, _logger, "GetEmployeeList");
            }

        }
        public async Task<ApiResponse<object>> ChangePasswordAsync(ChangeEmpPasswordDTO req)
        {
            try
            {
                if (req.NewPassword != req.ConfirmPassword)
                {
                    return new ApiResponse<object>(2,"New password and confirm password do not match !!");
                }
                var param = new DynamicParameters();
                param.Add("@ID", req.ID);
                param.Add("@OldPassword",CryptoHelper.Encrypt(req.OldPassword));
                param.Add("@NewPassword", CryptoHelper.Encrypt(req.NewPassword));
                param.Add("@Status", dbType:DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_change_employee_password", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage"); 
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Password changed successfully !!"),
                    2 => new ApiResponse<object>(2, "Old password is wrong !!"), 
                    3 => new ApiResponse<object>(3, "Old and new password cannot be same !!"), 
                    -1 => new ApiResponse<object>(-1, "User details could not be verified !!"), 
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch(Exception ex)
            {
                return ApiExceptionHandler.Handle<object>(ex, _logger, "AddEmployee");
            }
        }
    }
}
