using System.Text.Json.Serialization;

namespace PORTIMAGES.Application.Products.DTOs
{
    public class AssignProdConsigneeRequestDTO
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public string? EncId { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ConsigneeEmail { get; set; }
        public string? ConsigneeMobile { get; set; }
        public string? ConsigneeAddress { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
    }
}
