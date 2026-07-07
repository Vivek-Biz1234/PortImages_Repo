using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateEmployeeCommandHandler:IRequestHandler<UpdateEmployeeCommand,ApiResponse<object>>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public UpdateEmployeeCommandHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
            this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateEmployeeCommand request,CancellationToken cancellationToken)
        {
            var dto = new EmployeeMasterRequestDTO()
            {
                ID = request.ID,
                FullName = request.FullName,
                Email = request.Email,
                Mobile = request.Mobile,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy,
                Profile = request.Profile,
                ProfilePicture = request.ProfilePicture,
                DOB = request.DOB,
                DesignationId = request.DesignationId,
                RoleId = request.RoleId,
                Address = request.Address
            };
            return await _employeeMasterRepository.UpdateEmployeeAsync(dto);
        }
    }
}
