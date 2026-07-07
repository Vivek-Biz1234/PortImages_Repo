using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;


namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetINSOrganizationByIdQueryHandler : IRequestHandler<GetINSOrganizationByIdQuery, ApiResponse<INSOrganizationRequestDTO?>>
    {
        private readonly IINSOrganizationRepository _organizationRepository;

        public GetINSOrganizationByIdQueryHandler(IINSOrganizationRepository organizationRepository)
        {
            this._organizationRepository = organizationRepository;
        }
        public async Task<ApiResponse<INSOrganizationRequestDTO?>> Handle(GetINSOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _organizationRepository.GetINSOrganizationStatusByIdAsync(request.Id);
        }
    }
}
