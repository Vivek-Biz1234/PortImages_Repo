using MediatR;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddINSOrganizationCommand: IRequest<ApiResponse<object>>
    {
        public string OrganizationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
}
