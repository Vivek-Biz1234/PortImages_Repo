using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Queries;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class UserMasterController : Controller
    {
        private readonly IMediator _mediator;
        public UserMasterController(IMediator mediator)
        {
            this._mediator = mediator; 
        }
        #region User Registration
        public IActionResult UserMaster()
        {
            return View();
        }
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var response = await _mediator.Send(new GetUserListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int ID)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(ID));
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }
        #endregion
    }
}
