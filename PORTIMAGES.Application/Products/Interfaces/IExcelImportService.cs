using Microsoft.AspNetCore.Http;
using PORTIMAGES.Common.Responses;
using System.Data;

namespace PORTIMAGES.Application.Products.Interfaces
{
    public interface IExcelImportService
    {
        Task<ApiResponse<object>> ImportVehicleDataAsync(IFormFile file, int createdBy, int sourceId);
        Task<List<string>> GetExistingChassisViaFileAsync(DataTable dt);
    }
}
