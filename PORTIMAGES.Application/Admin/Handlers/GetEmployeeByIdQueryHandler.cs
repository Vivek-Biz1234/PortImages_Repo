using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetEmployeeByIdQueryHandler:IRequestHandler<GetEmployeeByIdQuery,ApiResponse<EmployeeMasterRequestDTO?>?>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public GetEmployeeByIdQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
            this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<EmployeeMasterRequestDTO?>?> Handle(GetEmployeeByIdQuery req,CancellationToken cancellationToken)
        {
            return await _employeeMasterRepository.GetEmployeeByIdAsync(req.Id);
        }
    }
}
