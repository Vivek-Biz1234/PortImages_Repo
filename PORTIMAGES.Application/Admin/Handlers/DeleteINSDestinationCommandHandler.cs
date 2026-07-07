using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteINSDestinationCommandHandler : IRequestHandler<DeleteINSDestinationCommand, ApiResponse<object>>
    {
        private readonly IINSDestinationRepository _destinationRepository;

        public DeleteINSDestinationCommandHandler(IINSDestinationRepository destinationRepository)
        {
            this._destinationRepository = destinationRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteINSDestinationCommand request, CancellationToken cancellationToken)
        {
            return await _destinationRepository.DeleteInventoryStatusAsync(request.ID, request.DeletedBy);
        }
    }
}
