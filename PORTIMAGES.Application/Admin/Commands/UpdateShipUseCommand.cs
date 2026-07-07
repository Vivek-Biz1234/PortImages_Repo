using MediatR;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateShipUseCommand : IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public string UseType { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
