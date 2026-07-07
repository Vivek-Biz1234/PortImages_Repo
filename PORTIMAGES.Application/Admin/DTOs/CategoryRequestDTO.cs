namespace PORTIMAGES.Application.Admin.DTOs
{
    public class CategoryRequestDTO
    {
        public long ID { get; set; }        
        public string? CategoryName { get; set; }
        public string? Titletag { get; set; }
        public string? KeywordTag { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
