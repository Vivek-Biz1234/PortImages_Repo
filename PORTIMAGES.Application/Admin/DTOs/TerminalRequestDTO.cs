namespace PORTIMAGES.Application.Admin.DTOs
{
    public class TerminalRequestDTO
    {
        public long ID { get; set; }
        public int PortId { get; set; }
        public string? TerminalName { get; set; }        
        public bool IsActive { get; set; } 
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
