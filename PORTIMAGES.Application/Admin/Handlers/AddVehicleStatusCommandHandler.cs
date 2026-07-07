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
    public class AddVehicleStatusCommandHandler : IRequestHandler<AddVehicleStatusCommand, ApiResponse<object>>
    {
        private readonly IVehicleStatusRepository _vehicalStatusRepository;
        public AddVehicleStatusCommandHandler(IVehicleStatusRepository vehicleStatusRepository)
        {
            this._vehicalStatusRepository = vehicleStatusRepository;
        }
 
        public async Task<ApiResponse<object>> Handle(AddVehicleStatusCommand request, CancellationToken cancellationToken)
        {

            var dto = new VehicleStatusRequestDTO()
            {
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };

                 return await _vehicalStatusRepository.AddVehicleStatusAsync(dto);
                
        }
    }
}
