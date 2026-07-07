using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddEmployeeCommand: IRequest<ApiResponse<object>>
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }  
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }

        //For additional info added later
        public string? Profile { get; set; }
        public string? DOB { get; set; }
        public int? DesignationId { get; set; }
        public int? RoleId { get; set; }
        public string? Address { get; set; }
    }
}
