using System.Text.Json.Serialization;

namespace PORTIMAGES.Application.Products.DTOs
{
    public class ProductResponseDTO
    {
        [JsonIgnore]
        public long ID { get; set; }
        public int TotalRows { get; set; }
        public string EncID { get; set; }
        public string ChassisNo { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public int ShipId { get; set; }
        public string ShipName { get; set; }
        public string TerminalName { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string MakerName { get; set; }

        public int? InventoryStatusId { get; set; }
        public string InventoryStatus { get; set; }

        public int? VehicleStatusId { get; set; }
        public string VehicleStatus { get; set; }

        // INS Section
        public int? InsOrganizationId { get; set; }
        public string InsOrganization { get; set; }

        public int? InsDestinationId { get; set; }
        public string InsDestination { get; set; }

        public int? InsStatusId { get; set; }
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

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public string? ProductSource { get; set; }
        public decimal InvoicePrice { get; set; }
    }
}
