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
    public class UpdateShipUseCommandHandler : IRequestHandler<UpdateShipUseCommand, ApiResponse<object>>
    {
        private readonly IShipUseRepository useRepository;

        public UpdateShipUseCommandHandler(IShipUseRepository shipUse)
        {
            this.useRepository = shipUse;
        }
       public async Task<ApiResponse<object>> Handle(UpdateShipUseCommand request, CancellationToken cancellationToken)
        {
            var dto = new ShipUseRequestDTO()
            {
                ID = request.ID,
                UseType = request.UseType,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };

            return await useRepository.UpdateShipUseStatusStatusAsync(dto);
        }
    }
}
