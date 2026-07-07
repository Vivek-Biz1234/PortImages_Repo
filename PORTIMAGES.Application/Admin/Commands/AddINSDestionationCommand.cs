using MediatR;
using PORTIMAGES.Common.Responses; 
namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddINSDestionationCommand: IRequest<ApiResponse<object>>
    {
        public string DestinationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
