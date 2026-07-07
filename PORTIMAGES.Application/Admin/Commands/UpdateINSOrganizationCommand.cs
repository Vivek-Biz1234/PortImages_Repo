using MediatR;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateINSOrganizationCommand: IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public string OrganizationName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
