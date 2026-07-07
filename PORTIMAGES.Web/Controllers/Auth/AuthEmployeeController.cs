using MediatR; 
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Auth.AuthEmployee.Commands;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;

namespace PORTIMAGES.Web.Controllers.Auth
{
    
    public class AuthEmployeeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthRepository _authService;

        public AuthEmployeeController(IMediator mediator,IAuthRepository authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpGet]
        [Route("StaffLogin",Name = "StaffLogin")]
        [Route("AuthEmployee/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {

            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _authService.SignOutAsync("StaffScheme");
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                await _authService.SignInAsync(result);
                return RedirectToAction(result.Action, result.Controller);
            }
            ModelState.AddModelError("", result.Message);
            return View(command);
        }

        [HttpPost] 
        [Route("StaffLogout", Name = "StaffLogout")]
        [Route("AuthEmployee/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync("StaffScheme");
            return Redirect("/StaffLogin");
        }
    }
}
 
