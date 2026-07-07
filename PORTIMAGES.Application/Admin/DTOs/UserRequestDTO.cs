namespace PORTIMAGES.Application.Admin.DTOs
{
    public class UserRequestDTO
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
