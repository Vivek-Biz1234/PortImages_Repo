using PORTIMAGES.Common.Interfaces;
using System.Text.Json.Serialization;
namespace PORTIMAGES.Application.Products.DTOs
{
    public class ProductConsigneeResponseDTO : IEncrypTableDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string? EncID { get; set; }
        public string? ChassisNo { get; set; } 
        public string? Shipper { get; set; } 
        public string? ModelName { get; set; }  
        public string? ConsigneeName { get; set; } 
        public string? ConsigneeEmail { get; set; } 
        public string? ConsigneeMobile { get; set; } 
        public string? ConsigneeAddress { get; set; } 
        public string? SourceName { get; set; } 
        public string? AssignBy { get; set; } 
        public string? AssignAt { get; set; }   
        public string? CreatedAt { get; set; }  
        public decimal? InvoicePrice { get; set; } 
    }
}
