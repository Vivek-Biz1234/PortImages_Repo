using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetUserByIdQueryHandler:IRequestHandler<GetUserByIdQuery,ApiResponse<UserRequestDTO?>?>
    {
        private readonly IUserMasterRepository _companyRepository;
        public GetUserByIdQueryHandler(IUserMasterRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public async Task<ApiResponse<UserRequestDTO?>?> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyByIdAsync(request.Id);
        } 
    }
}
