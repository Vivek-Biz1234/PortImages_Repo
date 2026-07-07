using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using PORTIMAGES.Application.Auth.AuthUser.Commands;
using PORTIMAGES.Application.Auth.AuthUser.DTOs;
namespace PORTIMAGES.Web.Controllers.Auth
{ 
    public class AuthUserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthRepository _authService; 
        public AuthUserController(IMediator mediator, IAuthRepository authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpGet]
        [Route("Login", Name = "UserLogin")]
        [Route("AuthUser/Login")]
        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                await _authService.SignInAsync(new()
                {
                    ID = result.UserID,
                    Name = result.Name,
                    Email = result.Email,
                    RoleName = result.RoleName,
                    Action = result.Action,
                    Controller = result.Controller,
                    Scheme=result.Scheme
                });               
                return RedirectToAction(result.Action, result.Controller);
            }
            ModelState.AddModelError("", result.Message);
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Logout")] 
        [Route("AuthUser/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync("UserScheme");
            return RedirectToRoute("UserLogin");
        }

        [HttpGet]
        [Route("Register", Name = "UserRegistration")]
        [Route("AuthUser/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        [Route("AuthUser/Register")]
        public async Task<IActionResult> Register(UserRegistrationDTO req)
        {
            if(!ModelState.IsValid)
            {
                return View(req);
            }
            AddUserCommand cmd = new AddUserCommand
            {
                UserName=req.FullName,
                Email=req.Email,
                Contact=req.Mobile,
                ContactPerson=req.ContactPerson,
                Address=req.Address,
                Password=req.Password,
                IsActive=true
            };

            var result = await _mediator.Send(cmd); 
            if (result.Status==1)
            {
                UserLoginCommand loginCmd = new UserLoginCommand
                {
                    Username=req.Email,
                    Password=req.Password
                };
                var loginResult = await _mediator.Send(loginCmd);
                if (loginResult.Success)
                {
                    await _authService.SignInAsync(new()
                    {
                        ID = loginResult.UserID,
                        Name = loginResult.Name,
                        Email = loginResult.Email,
                        RoleName = loginResult.RoleName,
                        Action = loginResult.Action,
                        Controller = loginResult.Controller,
                        Scheme = loginResult.Scheme
                    });
                    return RedirectToAction(loginResult.Action, loginResult.Controller);
                }
                else
                {
                    ModelState.AddModelError("", loginResult.Message);
                    return View(req);
                }
            }
            else
            {
                ModelState.AddModelError("", result.Message);               
                return View(req);
            } 
        }
         
    }
}
