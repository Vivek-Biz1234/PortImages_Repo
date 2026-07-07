using PORTIMAGES.Common.Interfaces;

namespace PORTIMAGES.Application.Products.DTOs
{
    public class FoundCarResponseDTO : IEncrypTableDTO
    {
        public int ID { get; set; }
        public string? EncID { get; set; }
        public string ChassisNo { get; set; }
        public string Shipper { get; set; }
        public string ModelName { get; set; }
        public int ShipId { get; set; }
        public string ShipName { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        public int TerminalId { get; set; }
        public string TerminalName { get; set; }
        public int OTerminalId { get; set; }
        public int ATerminalId { get; set; }
        public string OTerminalName { get; set; }
        public string ATerminalName { get; set; }
        public string FoundBy { get; set; }
        public string FoundAt { get; set; }
        public bool IsAvailable { get; set; }

        public int TotalRows { get; set; }
    }
}
