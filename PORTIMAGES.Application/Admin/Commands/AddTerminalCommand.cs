using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddTerminalCommand:IRequest<ApiResponse<Object>>
    {
        public int PortId { get; set; }
        public string TerminalName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
