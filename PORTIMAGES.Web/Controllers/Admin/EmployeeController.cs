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
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region Employee Master
        public IActionResult EmployeeMaster()
        {
            return View();
        }
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            var response = await _mediator.Send(new GetEmployeeListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(int ID)
        {
            var response = await _mediator.Send(new GetEmployeeByIdQuery(ID));
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion

        #region Created By Vivek on 12-May-2026
        public async Task<IActionResult> MyProfile()
        {
            int _id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(new GetEmployeeListQuery(_id));
            return View(response.Data.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMyProfile([FromForm] UpdateEmployeeCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            request.ID = Convert.ToInt32(request.UpdatedBy);
            request.IsActive = true;
            var result = await _mediator.Send(request);
            return Json(result);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeEmpPasswordCommand req)
        {
            req.ID= Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result=await _mediator.Send(req);
            return Json(result);
        }
        #endregion
    }
}
