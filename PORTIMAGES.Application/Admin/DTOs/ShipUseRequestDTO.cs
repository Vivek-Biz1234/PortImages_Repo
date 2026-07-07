 
namespace PORTIMAGES.Application.Admin.DTOs
{
    public class ShipUseRequestDTO
    {
        public long ID { get; set; }
        public string? UseType { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
