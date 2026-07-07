using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetVehicleStatusByIdQueryHandler : IRequestHandler<GetVehicleStatusByIdQuery, ApiResponse<VehicleStatusRequestDTO?>>
    {
        public readonly IVehicleStatusRepository _VehicleStatusRepository;

        public GetVehicleStatusByIdQueryHandler(IVehicleStatusRepository vehicalStatusRepository)
        {
            this._VehicleStatusRepository = vehicalStatusRepository;
        }
        public async Task<ApiResponse<VehicleStatusRequestDTO>?> Handle(GetVehicleStatusByIdQuery request, CancellationToken cancellationToken)
        {
            return await _VehicleStatusRepository.GetVehicleStatusByIdAsync(request.Id);
        }
    }
}
