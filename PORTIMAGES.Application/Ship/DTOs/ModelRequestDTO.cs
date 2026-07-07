namespace PORTIMAGES.Application.Ship.DTOs
{
    public class ModelRequestDTO
    {
        public int ID { get; set; }

        public int CategoryId { get; set; }
        public int MakerId { get; set; }

        public string ModelName { get; set; } = string.Empty;

        public string? Title { get; set; }
        public string? Keyword { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
