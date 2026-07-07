using PORTIMAGES.Common.Interfaces;

namespace PORTIMAGES.Application.Admin.DTOs
{
    public class EmployeeMasterResponseDTO: IEncrypTableDTO
    {
        public int ID { get; set; }
        public string? EncID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }         
        public bool IsActive { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        //
        public string? Profile { get; set; }
        public string? DOB { get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Address { get; set; }
    }
}
