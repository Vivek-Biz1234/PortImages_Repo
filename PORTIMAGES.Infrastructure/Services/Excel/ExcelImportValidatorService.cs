using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using PORTIMAGES.Common.DTOs.Import;
using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Interfaces.Excel; 

namespace PORTIMAGES.Infrastructure.Services.Excel
{
    public class ExcelImportValidatorService: IExcelImportValidatorService
    {
        public async Task<ImportValidationResultDTO> ValidateAsync(IFormFile file,IImportTemplateDefinition template)
        {
            var result = new ImportValidationResultDTO();

            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            using var workbook = new XLWorkbook(stream);

            var ws = workbook.Worksheet(template.SheetName);
            if (ws == null)
            {
                result.Errors.Add(new ImportValidationErrorDTO
                {
                    RowNumber = 0,
                    ColumnName = "Worksheet",
                    ErrorMessage = "Invalid worksheet."
                });
                return result;
            }

            int lastRow = ws.LastRowUsed().RowNumber();

            int totalRows = 0;

            var duplicateCheck = new HashSet<string>();

            // START FROM ROW 4
            for (int row = 4; row <= lastRow; row++)
            {
                bool isRowValid = true;

                totalRows++;

                for (int col = 0; col < template.Columns.Count; col++)
                {
                    var templateColumn = template.Columns[col];

                    var cell = ws.Cell(row, col + 1);

                    string value = cell.GetValue<string>().Trim();

                    // REQUIRED
                    if (templateColumn.IsRequired &&
                        string.IsNullOrWhiteSpace(value))
                    {
                        result.Errors.Add(new ImportValidationErrorDTO
                        {
                            RowNumber = row,
                            ColumnName = templateColumn.Header,
                            ErrorMessage = $"{templateColumn.Header} is required."
                        });

                        isRowValid = false;

                        continue;
                    }

                    // SKIP EMPTY OPTIONAL
                    if (string.IsNullOrWhiteSpace(value))
                        continue;

                    // TYPE VALIDATION
                    switch (templateColumn.ColumnType)
                    {
                        case ExcelColumnType.Number:

                            if (!int.TryParse(value, out _))
                            {
                                AddError(result, row,
                                    templateColumn.Header,
                                    "Invalid whole number.");

                                isRowValid = false;
                            }

                            break;

                        case ExcelColumnType.Decimal:

                            if (!decimal.TryParse(value, out _))
                            {
                                AddError(result, row,
                                    templateColumn.Header,
                                    "Invalid decimal value.");

                                isRowValid = false;
                            }

                            break;

                        case ExcelColumnType.Date:

                            if (!DateTime.TryParse(value, out _))
                            {
                                AddError(result, row,
                                    templateColumn.Header,
                                    "Invalid date.");

                                isRowValid = false;
                            }

                            break;

                        case ExcelColumnType.Boolean:

                            if (!bool.TryParse(value, out _))
                            {
                                AddError(result, row,
                                    templateColumn.Header,
                                    "Invalid boolean value.");

                                isRowValid = false;
                            }

                            break;

                        case ExcelColumnType.Dropdown:

                            if (templateColumn.DropdownValues != null &&
                                !templateColumn.DropdownValues
                                    .Contains(value))
                            {
                                AddError(result, row,
                                    templateColumn.Header,
                                    "Invalid dropdown value.");

                                isRowValid = false;
                            }

                            break;
                    }
                }

                // DUPLICATE CHASSIS CHECK
                var chassis = ws.Cell(row, 1).GetValue<string>();

                if (!string.IsNullOrWhiteSpace(chassis))
                {
                    if (!duplicateCheck.Add(chassis))
                    {
                        AddError(result,
                            row,
                            "ChassisNo",
                            "Duplicate chassis number.");

                        result.DuplicateRows++;

                        isRowValid = false;
                    }
                }

                if (!isRowValid)
                {
                    result.InvalidRows++;
                }
                else
                {
                    result.ValidRows++;
                }
            }

            result.TotalRows = totalRows;

            return result;
        }

        private void AddError(ImportValidationResultDTO result,int row,string column,string message)
        {
            result.Errors.Add(new ImportValidationErrorDTO
            {
                RowNumber = row,
                ColumnName = column,
                ErrorMessage = message
            });
        }
    }
}
