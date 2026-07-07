using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Menu.DTOs;
using PORTIMAGES.Application.Menu.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class MenusRepository : IMenusRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<MenusRepository> _logger;

        public MenusRepository(IDapperRepository dapper, ILogger<MenusRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        #region MAIN MENU
        #region ADD MAIN MENU
        public async Task<ApiResponse<object>> AddMainMenuAsync(AddMainMenuRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MainMenuName", request.MainMenuName);
                param.Add("@Icon", request.Icon);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_add_main_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Main menu added successfully!"),
                    2 => new ApiResponse<object>(2, "Main menu already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddMainMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region UPDATE MAIN MENU
        public async Task<ApiResponse<object>> UpdateMainMenuAsync(AddMainMenuRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MainMenuId", request.MainMenuId);
                param.Add("@MainMenuName", request.MainMenuName);
                param.Add("@Icon", request.Icon);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_update_main_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Main menu updated successfully!"),
                    2 => new ApiResponse<object>(2, "Main menu already exists!"),
                    -1 => new ApiResponse<object>(-1, "Main menu not found!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateMainMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region DELETE MAIN MENU
        public async Task<ApiResponse<object>> DeleteMainMenuAsync(int mainMenuId, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MainMenuId", mainMenuId);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_delete_main_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Main menu deleted successfully!"),
                    -1 => new ApiResponse<object>(-1, "Main menu not found!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteMainMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region GET MAIN MENU BY ID
        public async Task<ApiResponse<MainMenuResponseDTO?>> GetMainMenuByIdAsync(int mainMenuId)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<MainMenuResponseDTO>(
                    "dbo.usp_get_main_menu_by_id",
                    new { MainMenuId = mainMenuId },
                    CommandType.StoredProcedure
                );

                if (data == null)
                    return new ApiResponse<MainMenuResponseDTO?>(-1, "Main menu not found!", null);

                return new ApiResponse<MainMenuResponseDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetMainMenuById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<MainMenuResponseDTO?>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion

        #region GET MAIN MENU LIST
        public async Task<ApiResponse<List<MainMenuResponseDTO>>> GetMainMenuListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<MainMenuResponseDTO>(
                    "dbo.usp_get_main_menu_list",
                    null,
                    CommandType.StoredProcedure
                );

                var list = data.ToList();
                return new ApiResponse<List<MainMenuResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetMainMenuList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<MainMenuResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion
        #endregion

        #region SUBMENU
        #region ADD SUB MENU
        public async Task<ApiResponse<object>> AddSubMenuAsync(AddSubMenuRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MainMenuId", request.MainMenuId);
                param.Add("@SubMenuName", request.SubMenuName);
                param.Add("@Icon", request.Icon);
                param.Add("@ControllerName", request.ControllerName);
                param.Add("@ActionName", request.ActionName);
                param.Add("@ShortOrder", request.ShortOrder);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_add_sub_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Sub menu added successfully!"),
                    2 => new ApiResponse<object>(2, "Sub menu already exists!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "AddSubMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region UPDATE SUB MENU
        public async Task<ApiResponse<object>> UpdateSubMenuAsync(AddSubMenuRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SubMenuId", request.SubMenuId);
                param.Add("@MainMenuId", request.MainMenuId);
                param.Add("@SubMenuName", request.SubMenuName);
                param.Add("@Icon", request.Icon);
                param.Add("@ControllerName", request.ControllerName);
                param.Add("@ActionName", request.ActionName);
                param.Add("@ShortOrder", request.ShortOrder);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                param.Add("@ErrorMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_update_sub_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;
                string sqlError = param.Get<string>("@ErrorMessage");

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Sub menu updated successfully!"),
                    2 => new ApiResponse<object>(2, "Sub menu already exists!"),
                    -1 => new ApiResponse<object>(-1, "Sub menu not found!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong! " + sqlError)
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "UpdateSubMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region DELETE SUB MENU
        public async Task<ApiResponse<object>> DeleteSubMenuAsync(int subMenuId, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SubMenuId", subMenuId);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_delete_sub_menu", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Sub menu deleted successfully!", null),
                    -1 => new ApiResponse<object>(-1, "Sub menu not found!", null),
                    _ => new ApiResponse<object>(-99, "Something went wrong!", null)
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "DeleteSubMenu failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact support with Error ID: " + errorId, null);
            }
        }
        #endregion

        #region GET SUB MENU BY ID
        public async Task<ApiResponse<AddSubMenuRequestDTO?>> GetSubMenuByIdAsync(int subMenuId)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<AddSubMenuRequestDTO>(
                    "dbo.usp_get_sub_menu_by_id",
                    new { SubMenuId = subMenuId },
                    CommandType.StoredProcedure
                );

                if (data == null)
                    return new ApiResponse<AddSubMenuRequestDTO?>(-1, "Sub menu not found!", null);

                return new ApiResponse<AddSubMenuRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetSubMenuById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<AddSubMenuRequestDTO?>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId,
                    null
                );
            }
        }
        #endregion

        #region GET SUB MENU LIST
        public async Task<ApiResponse<object>> GetSubMenuListAsync(int mainMenuId)
        {
            try
            {
                var data = await _dapper.QueryAsync<SubMenuResponseDTO>(
                    "dbo.usp_get_sub_menu_list",
                    new { MainMenuId = mainMenuId },
                    CommandType.StoredProcedure
                );

                var list = data.ToList();
                return new ApiResponse<object>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "GetSubMenuList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }
        #endregion
        #endregion

        #region Assigne Menu
        public async Task<ApiResponse<EmployeeMenuPermissionResponseDTO>> GetEmployeeMenuPermissionsAsync(int empId)
        {
            using var multi = await _dapper.QueryMultipleAsync("dbo.usp_get_employee_menu_permissions", new { EmpId = empId }, commandType: CommandType.StoredProcedure);
            var employee = await multi.ReadFirstOrDefaultAsync<EmployeeInfoDTO>();
            var menus = (await multi.ReadAsync<EmployeeMenuPermissionDTO>()).ToList();

            var list = new EmployeeMenuPermissionResponseDTO
            {
                Employee = employee!,
                Menus = menus
            };
            return new ApiResponse<EmployeeMenuPermissionResponseDTO>(1, "Success", list);
        }

        public async Task<ApiResponse<object>> SaveEmployeeMenuPermissionsAsync(int empId, List<SaveEmployeeMenuDTO> menus, int createdBy)
        {
            try
            {
                await DeleteAssignedMenus(empId, createdBy);
                if (menus == null || menus.Count == 0)
                    return new ApiResponse<object>(-1, "No menu permissions provided.");

                foreach (var menu in menus)
                {
                    var param = new DynamicParameters();
                    param.Add("@EmpId", empId);
                    param.Add("@SubMenuId", menu.SubMenuId);
                    param.Add("@CanView", menu.CanView);
                    param.Add("@CanAdd", menu.CanAdd);
                    param.Add("@CanEdit", menu.CanEdit);
                    param.Add("@CanDelete", menu.CanDelete);
                    param.Add("@CreatedBy", createdBy);
                    param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                    await _dapper.ExecuteAsync("dbo.usp_save_employee_menu_permissions", param, CommandType.StoredProcedure);

                    short status = param.Get<short?>("@Status") ?? -99;

                    if (status != 1)
                    {
                        return new ApiResponse<object>(
                            -99,
                            "Failed to save one or more menu permissions."
                        );
                    }
                }

                return new ApiResponse<object>(
                    1,
                    "Employee menu permissions saved successfully!"
                );
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                //_logger.LogError(ex, "SaveEmployeeMenuPermissions failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId
                );
            }
        }

        public async Task<int> DeleteAssignedMenus(int EmpId, int DeletedBy)
        {
            var param = new DynamicParameters();
            param.Add("@EmpId", EmpId);
            param.Add("@DeletedBy", DeletedBy);
            return await _dapper.ExecuteAsync("dbo.usp_delete_Assigned_menu", param, CommandType.StoredProcedure);
        }

        #endregion
    }
}
