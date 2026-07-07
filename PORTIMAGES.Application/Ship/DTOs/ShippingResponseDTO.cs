namespace PORTIMAGES.Application.Ship.DTOs
{
    public class ShippingResponseDTO
    {
        public int ID { get; set; }

        public string Country { get; set; }
        public string ShippingName { get; set; }

        public string Email { get; set; }
        public string Contact { get; set; }
        public string Fax { get; set; }

        public string CCMail { get; set; }
        public string HOAddress { get; set; }
        public string BOAddress { get; set; }
        public string PersonInCharge { get; set; }

        public decimal? Rate { get; set; }
        public decimal? OpeningBal { get; set; }

        public bool IsActive { get; set; }

        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
