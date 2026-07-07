using Microsoft.AspNetCore.Http;

namespace PORTIMAGES.Application.Ship.DTOs
{
    public class MakerResponseDTO
    {
        public long ID { get; set; }

        public string? Country { get; set; }
        public string MakerName { get; set; }

        // LOGO PATH (NOT IFormFile)
        public string? Logo_SRC { get; set; }

        // SEO fields
        public string? Title { get; set; }
        public string? Keyword { get; set; }
        public string? Description { get; set; }
        public string? Canonical { get; set; }
         
        public bool IsActive { get; set; }

        public string? CreatedOn { get; set; }
        public string? UpdatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
