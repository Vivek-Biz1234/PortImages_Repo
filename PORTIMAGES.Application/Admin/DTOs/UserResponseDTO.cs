namespace PORTIMAGES.Application.Admin.DTOs
{
    public class UserResponseDTO
    {
        public int ID { get; set; }
        public string? UserName { get; set; } 
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } 
        public string? CreatedOn { get; set; }
        public string? UpdatedOn { get; set; } 
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
