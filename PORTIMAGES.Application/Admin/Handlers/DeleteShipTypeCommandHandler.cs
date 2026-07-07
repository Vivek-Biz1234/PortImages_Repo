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
    public class DeleteShipTypeCommandHandler : IRequestHandler<DeleteShipTypeCommand, ApiResponse<object>>
    {
        private readonly IShipTypeRepository shipTypeRepository;
        public DeleteShipTypeCommandHandler(IShipTypeRepository repository)
        {
            this.shipTypeRepository = repository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteShipTypeCommand request, CancellationToken cancellationToken)
        {
            return await shipTypeRepository.DeleteShipTypeStatusAsync(request.ID, request.DeletedBy);
        }
    }
}
