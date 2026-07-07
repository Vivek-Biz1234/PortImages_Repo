using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetVehicleStatusListQueryHandler : IRequestHandler<GetVehicleStatusListQuery, ApiResponse<List<VehicleStatusResponseDTO>>>
    {
        private readonly IVehicleStatusRepository _vehicleStatusRepository;
        public GetVehicleStatusListQueryHandler(IVehicleStatusRepository vehicleStatusRepository)
        {
            this._vehicleStatusRepository = vehicleStatusRepository;
        }

        public async Task<ApiResponse<List<VehicleStatusResponseDTO>>> Handle(GetVehicleStatusListQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleStatusRepository.GetVehicleStatusListAsync();
        }        
    }
}
