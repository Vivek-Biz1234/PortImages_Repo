namespace PORTIMAGES.Application.Ship.DTOs
{
    public class ShipResponseDTO
    {
        public int ID { get; set; }
        public string ShipName { get; set; }
        public string ShipType { get; set; }
        public string Shipping { get; set; }
        public string Port { get; set; }
        public string Terminal { get; set; }
        public string Country { get; set; }
        public string ShipUse { get; set; }

        public string DepDate { get; set; }
        public string ArrDate { get; set; }

        public decimal Freight { get; set; }
        public int? LCapacity { get; set; }

        public bool IsActive { get; set; }

        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
