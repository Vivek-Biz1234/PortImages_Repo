using MediatR;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddShipUseCommand : IRequest<ApiResponse<object>>
    {
        public string UseType { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
