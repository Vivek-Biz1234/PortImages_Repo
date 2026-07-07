using MediatR;
using PORTIMAGES.Common.Responses;
namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateVehicleStatusCommand: IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
