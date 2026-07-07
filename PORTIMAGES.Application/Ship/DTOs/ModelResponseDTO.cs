namespace PORTIMAGES.Application.Ship.DTOs
{
    public class ModelResponseDTO
    {
        public int ID { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Maker { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public string? Title { get; set; }
        public string? Keyword { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedOn { get; set; } = string.Empty;
        public string? UpdatedOn { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}
