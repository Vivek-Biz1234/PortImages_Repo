using PORTIMAGES.Common.Interfaces;

namespace PORTIMAGES.Application.Products.DTOs
{
    public class ProductBrokerResponseDTO : IEncrypTableDTO
    {
        public int ID { get; set; }
        public string? EncID { get; set; }
        public string? ChassisNo { get; set; }
        public string? Shipper { get; set; }
        public string? ModelName { get; set; }
        public string? BrokerName { get; set; }
        public string? AssignBy { get; set; }
        public string? AssignAt { get; set; }
        public string? CreatedAt { get; set; }
        public decimal? InvoicePrice { get; set; }
    }
}
