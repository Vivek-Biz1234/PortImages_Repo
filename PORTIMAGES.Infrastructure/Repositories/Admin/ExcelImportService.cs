using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Responses; 
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ExcelImportService: IExcelImportService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ExcelImportService> _logger;
        public ExcelImportService(IProductRepository productRepository, ILogger<ExcelImportService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<object>> ImportVehicleDataAsync(IFormFile file,int createdBy,int sourceId)
        {
            using var stream = new MemoryStream(); 
            await file.CopyToAsync(stream);

            using var workbook = new XLWorkbook(stream); 
            var ws = workbook.Worksheet("Vehicle Import"); 
            DataTable dt = new();

            dt.Columns.Add("ChassisNo");
            dt.Columns.Add("InvPrice");
            dt.Columns.Add("ClientId");
            dt.Columns.Add("ClientName");
            dt.Columns.Add("ShipName");
            dt.Columns.Add("ModelName");            
            dt.Columns.Add("YardInDate"); 
            dt.Columns.Add("createdBy"); 
            dt.Columns.Add("SourceId");

            int lastRow = ws.LastRowUsed().RowNumber();

            for (int row = 4; row <= lastRow; row++)
            {
                string chassisNo = ws.Cell(row, 1).GetString().Trim();

                if (string.IsNullOrWhiteSpace(chassisNo))
                    continue;
                int ClientId = createdBy; string ClientName = "", ShipName = "", ModelName = "";
                decimal.TryParse(ws.Cell(row, 2).GetString(),out decimal invPrice);
                DateTime.TryParse(ws.Cell(row, 3).GetString(),out DateTime yardInDate);
                string status = ws.Cell(row, 4).GetString().Trim();
                ShipName= ws.Cell(row, 4).GetString().Trim();
                ModelName= ws.Cell(row, 5).GetString().Trim();



                dt.Rows.Add(
                    chassisNo,
                    invPrice,
                    ClientId,
                    ClientName,
                    ShipName,
                    ModelName,                    
                    yardInDate.ToString("yyyy-MM-dd"), 
                    createdBy,
                    sourceId
                    );
            }
            var existingChassis =await _productRepository.GetExistingChassisViaFileAsync(dt);

            if (existingChassis.Any())
            {
                string message = 
                        $"These chassis already exist in system: " +
                        $"{string.Join(", ", existingChassis)}. " +
                        $"Please remove or change them and upload again.";
                return new ApiResponse<object>(2, message);               
            }


            return await _productRepository.ImportProductViaFileAsync(dt);
        }
        public async Task<List<string>> GetExistingChassisViaFileAsync(DataTable dt)
        {
            return await _productRepository.GetExistingChassisViaFileAsync(dt);
        }
    }
}
