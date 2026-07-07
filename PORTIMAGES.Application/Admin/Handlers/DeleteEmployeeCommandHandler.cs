using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteEmployeeCommandHandler:IRequestHandler<DeleteEmployeeCommand,ApiResponse<object>>
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public DeleteEmployeeCommandHandler(IEmployeeMasterRepository employeeMasterRepository)
        {
                this._employeeMasterRepository = employeeMasterRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteEmployeeCommand request,CancellationToken cancellationToken)
        {
            return await _employeeMasterRepository.DeleteEmployeeAsync(request.ID, request.DeletedBy);
        }
    }
}
