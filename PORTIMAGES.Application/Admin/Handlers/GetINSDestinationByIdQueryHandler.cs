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
    public class GetINSDestinationByIdQueryHandler : IRequestHandler<GetINSDestinationByIdQuery, ApiResponse<INSDestinationRequestDTO?>>
    {

        private readonly IINSDestinationRepository _destinationRepository;

        public GetINSDestinationByIdQueryHandler(IINSDestinationRepository destinationRepository)
        {
            this._destinationRepository = destinationRepository;
        }
        public async Task<ApiResponse<INSDestinationRequestDTO?>> Handle(GetINSDestinationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _destinationRepository.GetINSDestinationByIdAsync(request.Id);
        }
    }
}
