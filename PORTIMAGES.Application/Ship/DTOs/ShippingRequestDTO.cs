namespace PORTIMAGES.Application.Ship.DTOs
{
    namespace PORTIMAGES.Application.Ship.DTOs
    {
        public class ShippingRequestDTO
        {
            public int ID { get; set; }  
            public int CountryId { get; set; } 
            public int? ShipId { get; set; } 

            public string ShippingName { get; set; } 
            public string Email { get; set; } 
            public string Contact { get; set; } 
            public string Fax { get; set; } 

            public string PasswordHash { get; set; }
            public string CCMail { get; set; }

            public string HOAddress { get; set; } 
            public string BOAddress { get; set; } 
            public string PersonInCharge { get; set; }

            public decimal? Rate { get; set; }
            public decimal? OpeningBal { get; set; }

            public bool IsActive { get; set; }

            public int CreatedBy { get; set; } 
            public int UpdatedBy { get; set; }
        }
    }

}
