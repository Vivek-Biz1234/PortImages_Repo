namespace PORTIMAGES.Application.Products.DTOs
{
    public class FoundCarRequestDTO
    {
        public string ChassisNo { get; set; }
        public int ShipId { get; set; }
        public int TerminalId { get; set; }
        public string CarStatus { get; set; } // "1","0","-1"
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
