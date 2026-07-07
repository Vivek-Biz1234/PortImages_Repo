using PORTIMAGES.Common.DTOs.Import;
using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Interfaces.Excel;

namespace PORTIMAGES.Application.Common.ImportTemplates
{
    public class VehicleImportTemplate : IImportTemplateDefinition
    {
        private readonly Dictionary<string, List<string>> _dropdowns;
        public VehicleImportTemplate(Dictionary<string, List<string>> dropdowns=null)
        {
            _dropdowns = dropdowns ?? new Dictionary<string, List<string>>();
        }
        public string SheetName => "Vehicle Import";

        public string FileName => "vehicle-import-template.xlsx";

        public List<ExcelTemplateColumnDTO> Columns =>
        new()
        {
            new()
            {
                Header="ChassisNo",
                ColumnType=ExcelColumnType.Text,
                IsRequired=true,
                ExampleValue="ABC-123"
            },
            new()
            {
                Header="InvPrice",
                ColumnType=ExcelColumnType.Decimal,
                IsRequired=true,
                ExampleValue="100.50"
            },
            new()
            {
                Header="YardInDate",
                ColumnType =ExcelColumnType.Date,
                IsRequired=true,
                ExampleValue=DateTime.Now.ToString("yyyy-MM-dd"),
            },
            new()
            {
                Header = "ShipName",
                ColumnType = ExcelColumnType.Dropdown,
                DropdownValues = _dropdowns.ContainsKey("SHIP")? _dropdowns["SHIP"]: new List<string>()
            },
            new()
            {
                Header = "ModelName",
                ColumnType = ExcelColumnType.Dropdown,
                DropdownValues =  _dropdowns.ContainsKey("MODEL")? _dropdowns["MODEL"]: new List<string>()
            }
        };
    }
}
