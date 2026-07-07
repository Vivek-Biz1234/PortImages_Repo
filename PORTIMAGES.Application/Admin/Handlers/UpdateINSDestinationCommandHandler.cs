using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateINSDestinationCommandHandler : IRequestHandler<UpdateINSDestinationCommand, ApiResponse<object>>
    {
        public readonly IINSDestinationRepository _destinationRepository;


        public UpdateINSDestinationCommandHandler(IINSDestinationRepository destinationRepository)
        {
            this._destinationRepository = destinationRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateINSDestinationCommand request, CancellationToken cancellationToken)
        {
            var dto = new INSDestinationRequestDTO()
            {
                ID = request.ID,
                DestinationName = request.DestinationName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };

            return await _destinationRepository.UpdateINSDestinationAsync(dto);
        }
    }
}
