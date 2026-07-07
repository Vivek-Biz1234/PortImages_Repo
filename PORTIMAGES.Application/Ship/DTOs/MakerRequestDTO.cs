using Microsoft.AspNetCore.Http;

namespace PORTIMAGES.Application.Ship.DTOs
{
    public class MakerRequestDTO
    {
        public long ID { get; set; }
        public int? CountryId { get; set; }

        public string MakerName { get; set; } = null!;

        // FILE UPLOAD
        public IFormFile? Logo { get; set; }         // For new upload
        public string? Logo_SRC { get; set; }        // For existing image path

        // SEO fields
        public string? Title { get; set; }
        public string? Keyword { get; set; }
        public string? Description { get; set; }
        public string? Canonical { get; set; }

        // Content
        public string? Details { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
