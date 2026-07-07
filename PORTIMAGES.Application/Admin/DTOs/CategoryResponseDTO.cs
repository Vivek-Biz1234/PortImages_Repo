namespace PORTIMAGES.Application.Admin.DTOs
{
    public class CategoryResponseDTO
    {
        public long ID { get; set; }
        public string? CategoryName { get; set; }
        public string? TitleTag { get; set; }
        public string? KeywordTag { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedOn { get; set; }
        public string? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
