using MediatR;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.User.Commands
{
    public class AddInquiryCommand:IRequest<ApiResponse<object>>
    {
        public int ClientID { get; set; }
        public string? MobileNo { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
    }
}
