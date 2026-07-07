namespace PORTIMAGES.Application.Common.Models
{
    public class DropdownConfig
    {
        public string TableName { get; set; } = default!;
        public string ValueField { get; set; } = default!;
        public string TextField { get; set; } = default!;
        public string? FilterField { get; set; }
        public string? OrderBy { get; set; }
    }
}
