using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class ChangeEmpPasswordCommandHandler:IRequestHandler<ChangeEmpPasswordCommand,ApiResponse<object>>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public ChangeEmpPasswordCommandHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
            this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<object>> Handle(ChangeEmpPasswordCommand command,CancellationToken cancellationToken)
        {
            var req = new ChangeEmpPasswordDTO()
            {
                ID = command.ID,
                OldPassword = command.OldPassword,
                NewPassword = command.NewPassword,
                ConfirmPassword = command.ConfirmPassword
            };
            return await _employeeMasterRepository.ChangePasswordAsync(req);

        }
    }
}
