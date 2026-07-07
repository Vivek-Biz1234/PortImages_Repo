using MediatR;
using PORTIMAGES.Application.Admin.Commands; 
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand,ApiResponse<object>>
    {
        private readonly IUserMasterRepository _companyRepository;
        public DeleteUserCommandHandler(IUserMasterRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {            
            return await _companyRepository.DeleteCompanyAsync(request.ID,request.DeletedBy);
        }
    }
}
