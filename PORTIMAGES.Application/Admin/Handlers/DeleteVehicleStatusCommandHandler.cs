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
    public class DeleteVehicleStatusCommandHandler : IRequestHandler<DeleteVehicleStatusCommand, ApiResponse<object>>
    {
        private readonly IVehicleStatusRepository _VehicleStatusRepository;

        public DeleteVehicleStatusCommandHandler(IVehicleStatusRepository statusRepository)
        {
            _VehicleStatusRepository = statusRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteVehicleStatusCommand request, CancellationToken cancellationToken)
        {
            return await _VehicleStatusRepository.DeleteVehicleStatusAsync(request.ID, request.DeletedBy);
        }
    }
}
