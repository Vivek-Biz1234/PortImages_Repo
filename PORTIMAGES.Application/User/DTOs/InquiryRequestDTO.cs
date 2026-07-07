namespace PORTIMAGES.Application.User.DTOs
{
    public class InquiryRequestDTO
    {
        public long ID { get; set; }
        public int ClientID { get; set; }

        public string? Company { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }
        public string? MobileNo { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }     
    }
}
