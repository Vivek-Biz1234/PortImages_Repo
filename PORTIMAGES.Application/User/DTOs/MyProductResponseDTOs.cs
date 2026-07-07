using System.Text.Json.Serialization;

namespace PORTIMAGES.Application.User.DTOs
{
    public class MyProductResponseDTOs
    {
        [JsonIgnore]
        public long ID { get; set; }
        public string EncID { get; set; }
        public string ChassisNo { get; set; }        
        public string ShipName { get; set; } 
        public string TerminalName { get; set; } 
        public string ModelName { get; set; }
        public string MakerName { get; set; } 
        public string InventoryStatus { get; set; } 
        public string VehicleStatus { get; set; }

        // INS Section 
        public string InsOrganization { get; set; } 
        public string InsDestination { get; set; } 
        public string InsStatus { get; set; } 
        public string? InsDate { get; set; } 
        public string NFCNO { get; set; }
        public string REFNO { get; set; }
        public string YardInNo { get; set; }
        public string YardInPlace { get; set; }

        public string? YardInDate { get; set; }
        public string? YardOutDate { get; set; }

        public string? ScheduledShippingDate { get; set; }
        public string? ShippingDate { get; set; }
        public string VoyageNo { get; set; }

        public int StoragePeriod { get; set; }
        public string ContainerNo { get; set; }
        public int? Mileage { get; set; }
        public string Location { get; set; } 
        public bool InnerCargo { get; set; }
        public string Notes { get; set; }
        public string ReasonFailure { get; set; } 
        public int ImageCount { get; set; }
        public string FirstImagePath { get; set; }

    }
}
