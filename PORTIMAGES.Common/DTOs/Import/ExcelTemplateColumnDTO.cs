using PORTIMAGES.Common.Enums;

namespace PORTIMAGES.Common.DTOs.Import
{
    public class ExcelTemplateColumnDTO
    {
        public string Header { get; set; }

        public ExcelColumnType ColumnType { get; set; }

        public bool IsRequired { get; set; } 
        public double? MinValue { get; set; }

        public double? MaxValue { get; set; } 
        public string? ExampleValue { get; set; }
        public List<string>? DropdownValues { get; set; }
        public string? DropdownSheetName { get; set; }
    }
}
