using PORTIMAGES.Common.DTOs.Import;
namespace PORTIMAGES.Common.Interfaces.Excel
{
    public interface IImportTemplateService
    {
        byte[] GenerateTemplate(string sheetName,List<ExcelTemplateColumnDTO> columns);
    }
}
