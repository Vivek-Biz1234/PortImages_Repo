using Microsoft.AspNetCore.Http; 
using PORTIMAGES.Common.DTOs.Import; 

namespace PORTIMAGES.Common.Interfaces.Excel
{
    public interface IExcelImportValidatorService
    {
        Task<ImportValidationResultDTO> ValidateAsync(IFormFile file,IImportTemplateDefinition template);
    }
}
