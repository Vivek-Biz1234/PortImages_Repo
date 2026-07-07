using PORTIMAGES.Application.Menu.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Menu.Interfaces
{
    public interface IMenusRepository
    {
        #region MAINMENU
        Task<ApiResponse<object>> AddMainMenuAsync(AddMainMenuRequestDTO request);
        Task<ApiResponse<object>> UpdateMainMenuAsync(AddMainMenuRequestDTO request);
        Task<ApiResponse<object>> DeleteMainMenuAsync(int mainMenuId, int deletedBy);
        Task<ApiResponse<MainMenuResponseDTO?>> GetMainMenuByIdAsync(int mainMenuId);
        Task<ApiResponse<List<MainMenuResponseDTO>>> GetMainMenuListAsync();
        #endregion

        #region SUBMENU
        Task<ApiResponse<object>> AddSubMenuAsync(AddSubMenuRequestDTO dto);
        Task<ApiResponse<object>> UpdateSubMenuAsync(AddSubMenuRequestDTO dto);
        Task<ApiResponse<object>> GetSubMenuListAsync(int mainMenuId);
        Task<ApiResponse<AddSubMenuRequestDTO?>> GetSubMenuByIdAsync(int subMenuId);
        Task<ApiResponse<object>> DeleteSubMenuAsync(int subMenuId, int deletedBy);
        #endregion

        #region AssignMenu
        Task<ApiResponse<EmployeeMenuPermissionResponseDTO>>GetEmployeeMenuPermissionsAsync(int empId); 
        Task<ApiResponse<object>>SaveEmployeeMenuPermissionsAsync(int empId,List<SaveEmployeeMenuDTO> menus,int createdBy);
        #endregion
    }
}
