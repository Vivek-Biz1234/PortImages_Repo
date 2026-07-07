using MediatR;
using PORTIMAGES.Application.Auth.AuthEmployee.Commands;
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;

namespace PORTIMAGES.Application.Auth.AuthEmployee.Handlers
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand,LoginResultDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public LoginCommandHandler(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public async Task<LoginResultDTO> Handle(LoginCommand request,CancellationToken cancellationToken)
        {
            return await _employeeRepository.LoginAsync(request.Username, request.Password);
        }
    }
}
