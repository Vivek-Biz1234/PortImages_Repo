using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetEmployeeListQueryHandler:IRequestHandler<GetEmployeeListQuery,ApiResponse<List<EmployeeMasterResponseDTO>>>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public GetEmployeeListQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
            this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<List<EmployeeMasterResponseDTO>>> Handle(GetEmployeeListQuery query,CancellationToken cancellationToken)
        {
            return await _employeeMasterRepository.GetEmployeeListAsync(query.Id);
        }
    }
}
