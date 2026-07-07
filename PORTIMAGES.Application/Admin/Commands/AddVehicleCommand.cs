using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public  class AddVehicleCommand : IRequest<ApiResponse<object>>
    {
        public string VehicleName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
