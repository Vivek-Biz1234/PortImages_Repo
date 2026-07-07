namespace PORTIMAGES.Application.Ship.DTOs
{
    public class PortRequestDTO
    {
        public int ID { get; set; }
        public int CountryId { get; set; }
        public string PortName { get; set; }
        public string SName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }       
    }
}
