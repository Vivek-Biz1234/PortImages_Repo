using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
using System;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetINSDestinationListQueryHandler : IRequestHandler<GetINSDestinationListQuery, ApiResponse<List<INSDestinationResponseDTO>>>
    {
        private readonly IINSDestinationRepository _INSDestinationRepository;

        public GetINSDestinationListQueryHandler(IINSDestinationRepository INSDestinationRepository)
        {
            this._INSDestinationRepository = INSDestinationRepository;
        }
        public async Task<ApiResponse<List<INSDestinationResponseDTO>>> Handle(GetINSDestinationListQuery request, CancellationToken cancellationToken)
        {
            return await _INSDestinationRepository.GetINSDestionationListAsync();
        }
    }
}
