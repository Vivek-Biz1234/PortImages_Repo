namespace PORTIMAGES.Application.Admin.DTOs
{
    public class InventoryStatusResponseDTO
    {
        public int ID { get; set; }
        public string? StatusName { get; set; }
        public bool IsActive { get; set; }

        public string? CreatedOn { get; set; }
        public string? UpdatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
