using Microsoft.AspNetCore.Http; 
namespace PORTIMAGES.Application.Products.DTOs
{
    public class UploadProductImageRequesDTO
    {
        public long? ID { get; set; }
        public long ProductId { get; set; }
        public string EncID { get; set; }
        public string ComponentName { get; set; }
        public decimal ComponentWeight { get; set; }
        public string WeightUnit { get; set; }
        
        //public IFormFile? Pimage { get; set; }
        public List<IFormFile> Pimages { get; set; } = new();
        public string? Pimage_SRC { get; set; } 
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
