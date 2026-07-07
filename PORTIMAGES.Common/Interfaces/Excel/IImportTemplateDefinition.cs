using PORTIMAGES.Common.DTOs.Import; 
namespace PORTIMAGES.Common.Interfaces.Excel
{
    public interface IImportTemplateDefinition
    {
        string SheetName { get; }
        string FileName { get; }
        List<ExcelTemplateColumnDTO> Columns { get; }
    }
}
