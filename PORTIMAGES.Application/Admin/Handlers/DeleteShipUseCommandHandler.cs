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
    public class DeleteShipUseCommandHandler : IRequestHandler<DeleteShipUseCommand, ApiResponse<object>>
    {
        private readonly IShipUseRepository shipUseRepository;

        public DeleteShipUseCommandHandler(IShipUseRepository shipUseRepository)
        {
            this.shipUseRepository = shipUseRepository;
        }
       public async Task<ApiResponse<object>> Handle(DeleteShipUseCommand request, CancellationToken cancellationToken)
       {

            return await shipUseRepository.DeleteShipUseStatusAsync(request.ID, request.DeletedBy);
       }

       
    }
}
