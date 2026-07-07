namespace PORTIMAGES.Application.Admin.DTOs
{
    public class INSDestinationRequestDTO
    {
        public long ID { get; set; }
        public string? DestinationName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
