using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{    
    [Authorize(Roles ="Admin,Staff")] 
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class AdminController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        public AdminController(IMenuRepository menuRepository)
        {
            this._menuRepository = menuRepository;
        }
        public IActionResult DashBoard()
        {
            return View();
        } 
        public async Task<IActionResult> LoadMenu()
        {
            var empId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                empId = Convert.ToInt32(userIdClaim);
            }
            var menuList = await _menuRepository.GetMenuByUserIdAsync(empId);
            return PartialView("~/Views/Shared/Admin/_AdminMenu.cshtml", menuList);
        }
    }
}
