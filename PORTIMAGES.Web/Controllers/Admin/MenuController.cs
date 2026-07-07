using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Menu.DTOs;
using PORTIMAGES.Application.Menu.Interfaces;
using PORTIMAGES.Common.Helpers;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class MenuController : Controller
    {
        private readonly IMenusRepository _menuRepository;

        public MenuController(IMenusRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        #region MainMenu
        public IActionResult MainMenu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMainMenu([FromBody] AddMainMenuRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _menuRepository.AddMainMenuAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMainMenu([FromBody] AddMainMenuRequestDTO dto)
        {
            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _menuRepository.UpdateMainMenuAsync(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainMenuList()
        {
            var result = await _menuRepository.GetMainMenuListAsync();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainMenuById(int mainMenuId)
        {
            var response = await _menuRepository.GetMainMenuByIdAsync(mainMenuId);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMainMenu(int mainMenuId)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _menuRepository.DeleteMainMenuAsync(mainMenuId, deletedBy);
            return Json(result);
        }
        #endregion

        #region SUBMENU 

        public IActionResult SubMenu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubMenu([FromBody] AddSubMenuRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _menuRepository.AddSubMenuAsync(dto); // Using same repo
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubMenu([FromBody] AddSubMenuRequestDTO dto)
        {
            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // dto.SubMenuId should already be set from front-end
            var result = await _menuRepository.UpdateSubMenuAsync(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubMenuList(int mainMenuId)
        {
            var result = await _menuRepository.GetSubMenuListAsync(mainMenuId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubMenuById(int subMenuId)
        {
            var result = await _menuRepository.GetSubMenuByIdAsync(subMenuId);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubMenu(int subMenuId)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _menuRepository.DeleteSubMenuAsync(subMenuId, deletedBy);
            return Json(result);
        }
        #endregion

        #region Assign Menu
        public IActionResult AssignMenu()
        { 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeMenuPermissions(string empid)
        {
            int _eid = int.Parse(CryptoHelper.Decrypt(empid));
            var result = await _menuRepository.GetEmployeeMenuPermissionsAsync(_eid);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeMenuPermissions(string empid, [FromBody] List<SaveEmployeeMenuDTO> menus)
        {
            int createdBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            int _eid = int.Parse(CryptoHelper.Decrypt(empid));
            var result = await _menuRepository.SaveEmployeeMenuPermissionsAsync(_eid, menus, createdBy); 
            return Json(result);
        }
        #endregion
    }
}
