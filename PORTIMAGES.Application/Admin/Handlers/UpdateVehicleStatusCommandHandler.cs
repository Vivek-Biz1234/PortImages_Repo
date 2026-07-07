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
    public class UpdateVehicleStatusCommandHandler : IRequestHandler<UpdateVehicleStatusCommand, ApiResponse<object>>
    {
        private readonly IVehicleStatusRepository _inventoryStatusRepository;

        public UpdateVehicleStatusCommandHandler(IVehicleStatusRepository inventoryStatusRepository)
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateVehicleStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = new VehicleStatusRequestDTO()
            {
                ID = request.ID,
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };
            return await _inventoryStatusRepository.UpdateVehicleStatusAsync(dto);
        }
    }
}
