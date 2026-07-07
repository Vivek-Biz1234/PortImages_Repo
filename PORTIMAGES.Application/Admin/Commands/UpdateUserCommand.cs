using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateUserCommand:IRequest<ApiResponse<object>>
    {
        public int ID{ get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
