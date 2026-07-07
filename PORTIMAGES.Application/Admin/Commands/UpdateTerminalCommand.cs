using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateTerminalCommand:IRequest<ApiResponse<object>>
    {
        public long ID{ get; set; }
        public int PortId { get; set; }
        public string TerminalName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
