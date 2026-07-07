using MediatR;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateShipTypeCommand:IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
