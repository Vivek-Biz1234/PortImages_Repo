using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddInventoryStatusCommand:IRequest<ApiResponse<object>>
    {
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
