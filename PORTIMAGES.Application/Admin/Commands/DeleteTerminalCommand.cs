using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class DeleteTerminalCommand:IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public int DeletedBy { get; set; }
    }
}
