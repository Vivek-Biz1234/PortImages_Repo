using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Common.ImportTemplates;
using PORTIMAGES.Application.Common.Interfaces;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Interfaces.Excel;
using System.Data;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Common
{
    public class ImportController : Controller
    {
        private readonly IImportTemplateService _importTemplateService;
        private readonly IExcelImportValidatorService _validator;
        private readonly IExcelImportService _excelImportService;
        private readonly IDropdownRepository _dropdownRepository;
        public ImportController(IImportTemplateService importTemplateService, IExcelImportValidatorService validator, IExcelImportService excelImportService, IDropdownRepository dropdownRepository)
        {
            _importTemplateService = importTemplateService;
            _validator = validator;
            _excelImportService = excelImportService;
            _dropdownRepository = dropdownRepository;
        }
        [Authorize(Roles = "Admin,Staff,User")]
        [Authorize(AuthenticationSchemes = "StaffScheme,UserScheme")]
        [HttpGet]
        public async Task<IActionResult> DownloadVehicleTemplate()
        {
            var dropdowns= new Dictionary<string, List<string>>();
            var ships = await _dropdownRepository.GetDropdownValuesAsync("SHIP",null);
            dropdowns.Add("SHIP", ships.Select(x => x.Name).ToList());

            var models = await _dropdownRepository.GetDropdownValuesAsync("MODEL", null);
            dropdowns.Add("MODEL", models.Select(x => x.Name).ToList());


            var template = new VehicleImportTemplate(dropdowns);
            var fileBytes = _importTemplateService.GenerateTemplate(template.SheetName, template.Columns);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", template.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateVehicleImport(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { status = -1, message = "Please upload excel file." });
                }
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".xlsx" && extension != ".xls")
                {
                    return Json(new { status = -1, message = "Only excel files are allowed." });
                }
                var dropdowns = new Dictionary<string, List<string>>();
                var ships = await _dropdownRepository.GetDropdownValuesAsync("SHIP", null);
                dropdowns.Add("SHIP", ships.Select(x => x.Name).ToList());

                var models = await _dropdownRepository.GetDropdownValuesAsync("MODEL", null);
                dropdowns.Add("MODEL", models.Select(x => x.Name).ToList());
                var template = new VehicleImportTemplate(dropdowns);
                var result = await _validator.ValidateAsync(file, template);

                return Json(new { status = 1, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { status = -99, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff,User")]
        [Authorize(AuthenticationSchemes = "StaffScheme,UserScheme")]
        public async Task<IActionResult> ImportVehicleData(IFormFile file)
        {
            try
            {
                int createdBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                int sourceId = MasterSourceHelper.UserPanel;
                var result = await _excelImportService.ImportVehicleDataAsync(file, createdBy, sourceId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = -99,
                    message = ex.Message
                });
            }
        }
    }
}
