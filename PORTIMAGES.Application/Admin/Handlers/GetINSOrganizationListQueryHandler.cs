using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetINSOrganizationListQueryHandler : IRequestHandler<GetINSOrganizationListQuery, ApiResponse<List<INSOrganizationResponseDTO>>>
    {
        private readonly IINSOrganizationRepository _iNSOrganizationRepository;

        public GetINSOrganizationListQueryHandler(IINSOrganizationRepository organizationRepository)
        {
            this._iNSOrganizationRepository = organizationRepository;
        }
        public async Task<ApiResponse<List<INSOrganizationResponseDTO>>> Handle(GetINSOrganizationListQuery request, CancellationToken cancellationToken)
        {
            return await _iNSOrganizationRepository.GetINSOrganizationListAsync();
        }
    }
}
