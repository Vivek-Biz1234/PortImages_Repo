namespace PORTIMAGES.Application.Admin.DTOs
{
    public class InventoryStatusRequestDTO
    {
        public long ID { get; set; }
        public string? StatusName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
