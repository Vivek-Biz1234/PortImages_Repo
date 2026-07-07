using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, ApiResponse<object>>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public AddEmployeeCommandHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
            this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dto = new EmployeeMasterRequestDTO()
            {
                FullName = request.FullName,
                Email = request.Email,
                Mobile = request.Mobile,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
                Profile=request.Profile,
                DOB=request.DOB,
                DesignationId=request.DesignationId,
                RoleId=request.RoleId,
                Address=request.Address
            };
            return await _employeeMasterRepository.AddEmployeeAsync(dto);
        }
    }
}
