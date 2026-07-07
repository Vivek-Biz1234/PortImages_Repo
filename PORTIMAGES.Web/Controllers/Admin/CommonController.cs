using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Common.Helpers;
using PORTIMAGES.Application.Common.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Web.Controllers.Admin
{
    [Area("Admin")]
    [Route("Admin/Common")] 
    [Authorize(Roles = "Admin,Staff,User")]
    [Authorize(AuthenticationSchemes = "StaffScheme,UserScheme")]
    public class CommonController : Controller
    {
        private readonly IDropdownRepository _dropdownRepository;
        public CommonController(IDropdownRepository dropdownRepository)
        {
                this._dropdownRepository = dropdownRepository;
        }

        [HttpGet("GetDropdown")]
        public async Task<IActionResult> GetDropdown(string key, int? parentId)
        {
            try
            {
                var config = DropdownMappings.Get(key);

                var data = await _dropdownRepository.GetAsync(
                    config.TableName,
                    config.ValueField,
                    config.TextField,
                    config.FilterField,
                    parentId,
                    config.OrderBy
                );

                return Json(new ApiResponse<object>(1, "Success", data));
            }
            catch (Exception ex)
            {
                return Json(new ApiResponse<object>(-99, ex.Message));
            }
        }
    }
}
