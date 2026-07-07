namespace PORTIMAGES.Application.Products.DTOs
{
    public class ViewProductDetailsDTO
    {
        public string ProductId { get; set; }
        public string TerminalName { get; set; }
        public string ChassisNo { get; set; }
        public string ShipName { get; set; }
        public string YardInDate { get; set; }
        public string InventoryStatus { get; set; }
        public string MakerName { get; set; }
        public string ModelName { get; set; } 
        public string ScheduledShippingDate { get; set; }
        public string ShippingDate { get; set; }
        public string VoyageNo { get; set; } 
        public string Notes { get; set; } 
        public int StoragePeriod { get; set; } 
        public string RadiationDose { get; set; }
        public List<string> Images { get; set; } = new();
    }
}
