using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetUserListQueryHandler:IRequestHandler<GetUserListQuery,ApiResponse<List<UserResponseDTO>>>
    {
        private readonly IUserMasterRepository _companyRepository;
        public GetUserListQueryHandler(IUserMasterRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public async Task<ApiResponse<List<UserResponseDTO>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyListAsync();
        }
    }
}
