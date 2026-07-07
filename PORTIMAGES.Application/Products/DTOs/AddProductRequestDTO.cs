using PORTIMAGES.Common.Helpers;

namespace PORTIMAGES.Application.Products.DTOs
{
    public class AddProductRequestDTO
    {
        public long? ID { get; set; }
        public string? EncID { get; set; }
        public string ChassisNo { get; set; }
        public int ClientId { get; set; }
        public int ShipId { get; set; }
        public int ModelId { get; set; }
        public int InventoryStatusId { get; set; }
        public int VehicleStatusId { get; set; }
        // INS Details
        public int InsOrganizationId { get; set; }
        public int InsDestinationId { get; set; }
        public int InsStatusId { get; set; }
        public string? InsDate { get; set; }

        public string? NFCNO { get; set; }
        public string? REFNO { get; set; }
        public string? YardInNo { get; set; }
        public string? YardInPlace { get; set; }

        public string? YardInDate { get; set; }
        public string? YardOutDate { get; set; }

        public string? ScheduledShippingDate { get; set; }
        public string? ShippingDate { get; set; }
        public string? VoyageNo { get; set; }

        public int? StoragePeriod { get; set; }
        public string? ContainerNo { get; set; }
        public int? Mileage { get; set; }
        public string? Location { get; set; }

        public bool? InnerCargo { get; set; }
        public string? Notes { get; set; }
        public string? ReasonFailure { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public decimal InvoicePrice { get; set; }
        public int SourceId { get; set; } = MasterSourceHelper.AdminPanel;
    }
}
