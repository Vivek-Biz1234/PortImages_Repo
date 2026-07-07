namespace PORTIMAGES.Application.Ship.DTOs
{
    public class ShipRequestDTO
    {
        public int ID { get; set; }
        public int ShipTypeId { get; set; }
        public int ShippingId { get; set; }
        public int PortId { get; set; }
        public int TerminalId { get; set; }
        public int CountryId { get; set; }
        public int ShipUseId { get; set; }

        public string ShipName { get; set; }
        public DateTime DepDate { get; set; }
        public DateTime ArrDate { get; set; }

        public decimal Freight { get; set; }
        public int? LCapacity { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
