using Microsoft.AspNetCore.Http;

namespace PORTIMAGES.Application.Admin.DTOs
{
    public class EmployeeMasterRequestDTO
    {
        public int ID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }      
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //For additional info added later
        public string? Profile { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? DOB { get; set; }
        public int? DesignationId { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
    }
}
