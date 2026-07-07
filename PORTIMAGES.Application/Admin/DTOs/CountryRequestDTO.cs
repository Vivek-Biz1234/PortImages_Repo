
namespace PORTIMAGES.Application.Admin.DTOs
{
    public class CountryRequestDTO
    {
        public long ID { get; set; }
        public string? CountryName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy{get; set;}

    }
}
