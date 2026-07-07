namespace PORTIMAGES.Application.Ship.DTOs
{
    public class PortResponseDTO
    {
        public int ID { get; set; }

        public string Country { get; set; }
        public string PortName { get; set; }
        public string SName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
