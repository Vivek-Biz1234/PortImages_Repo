using ClosedXML.Excel;
using PORTIMAGES.Common.DTOs.Import;
using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Interfaces.Excel;

namespace PORTIMAGES.Infrastructure.Services.Excel
{
    public class ImportTemplateService : IImportTemplateService
    {
        public byte[] GenerateTemplate(string sheetName, List<ExcelTemplateColumnDTO> columns)
        {
            using var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add(sheetName);
            var masterSheet = workbook.Worksheets.Add("MasterData");
            masterSheet.Visibility = XLWorksheetVisibility.VeryHidden;

            int totalColumns = columns.Count;
            int lastCol = 0;


            // =========================
            // HEADERS
            // =========================

            for (int i = 0; i < columns.Count; i++)
            {
                int col = i + 1;
                var item = columns[i];
                if (item.ColumnType == ExcelColumnType.Dropdown && item.DropdownValues != null && item.DropdownValues.Any())
                {
                    for (int j = 0; j < item.DropdownValues.Count; j++)
                    {
                        masterSheet.Cell(j + 1, col).Value = item.DropdownValues[j];
                    }
                }

                var headerCell = ws.Cell(2, col);

                headerCell.Value = item.Header;

                headerCell.Style.Font.Bold = true;
                headerCell.Style.Font.FontColor = XLColor.White;
                headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                headerCell.Style.Font.FontName = "Palatino Linotype";

                //headerCell.Style.Fill.BackgroundColor = item.IsRequired
                //    ? XLColor.FromHtml("#D9534F")
                //    : XLColor.FromHtml("#5BC0DE");

                headerCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#5b9ad5");

                // FULL BORDER
                headerCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                headerCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                headerCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                headerCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                headerCell.Style.Border.TopBorderColor = XLColor.Black;
                headerCell.Style.Border.BottomBorderColor = XLColor.Black;
                headerCell.Style.Border.LeftBorderColor = XLColor.Black;
                headerCell.Style.Border.RightBorderColor = XLColor.Black;

                // =========================
                // EXAMPLE ROW
                // =========================

                if (!string.IsNullOrEmpty(item.ExampleValue))
                {
                    var exampleCell = ws.Cell(3, col);

                    exampleCell.Value = item.ExampleValue;

                    exampleCell.Style.Font.Italic = true;
                    exampleCell.Style.Font.FontColor = XLColor.Gray;

                    exampleCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F8F9FA");

                    exampleCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    exampleCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    exampleCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    exampleCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    exampleCell.Style.Font.FontName = "Palatino Linotype";

                    exampleCell.Style.Border.TopBorderColor = XLColor.Black;
                    exampleCell.Style.Border.BottomBorderColor = XLColor.Black;
                    exampleCell.Style.Border.LeftBorderColor = XLColor.Black;
                    exampleCell.Style.Border.RightBorderColor = XLColor.Black;
                }



                // VALIDATION
                ApplyValidation(ws, col, item);
                lastCol = col;
            }
            // =========================
            // INPUT AREA BORDER
            // =========================

            var dataRange = ws.Range(1, 1, 1000, lastCol);

            dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

            dataRange.Style.Border.TopBorderColor = XLColor.Black;
            dataRange.Style.Border.BottomBorderColor = XLColor.Black;
            dataRange.Style.Border.LeftBorderColor = XLColor.Black;
            dataRange.Style.Border.RightBorderColor = XLColor.Black;

            // =========================
            // TITLE
            // =========================

            // =========================
            // TITLE
            // =========================

            var titleRange = ws.Range(1, 1, 1, totalColumns);

            titleRange.Merge();

            titleRange.Value = $"{sheetName} Template";

            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 14;
            titleRange.Style.Font.FontColor = XLColor.White;

            titleRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#70ad47");

            titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            titleRange.Style.Alignment.WrapText = false;

            // BORDER
            titleRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            titleRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            titleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            titleRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

            titleRange.Style.Border.TopBorderColor = XLColor.Black;
            titleRange.Style.Border.BottomBorderColor = XLColor.Black;
            titleRange.Style.Border.LeftBorderColor = XLColor.Black;
            titleRange.Style.Border.RightBorderColor = XLColor.Black;

            // HEIGHT
            ws.Row(1).Height = 28;

            // =========================
            // FIXED WIDTH
            // =========================

            ws.Columns().AdjustToContents();

            for (int i = 1; i <= totalColumns; i++)
            {
                ws.Column(i).AdjustToContents();

                if (ws.Column(i).Width < 20)
                {
                    ws.Column(i).Width = 20;
                }

                if (ws.Column(i).Width > 35)
                {
                    ws.Column(i).Width = 35;
                }
            }

            // HEADER HEIGHT
            ws.Row(2).Height = 24;
            ws.Row(3).Height = 22;

            // FREEZE HEADER
            ws.SheetView.FreezeRows(2);
            ws.Style.Alignment.WrapText = false;

            // =========================
            // SAVE
            // =========================

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private void ApplyValidation(IXLWorksheet ws, int columnNumber, ExcelTemplateColumnDTO item)
        {
            string columnLatter = GetExcelColumnName(columnNumber);
            var range = ws.Range($"{columnLatter}3:{columnLatter}1000");
            var validation = range.CreateDataValidation();
            switch (item.ColumnType)
            {
                case ExcelColumnType.Number:
                    validation.WholeNumber.Between(0, 999999999);
                    validation.InputTitle = "Number Only";
                    validation.InputMessage = "Enter whole nunber only.";
                    validation.ErrorTitle = "Invalid Number";
                    validation.ErrorMessage = "Only whole numbers are allowed.";
                    validation.ShowErrorMessage = true;
                    break;
                case ExcelColumnType.Decimal:
                    validation.Decimal.Between(0, 999999999);
                    validation.InputTitle = "Decimal Only";
                    validation.InputMessage = "Enter decimal value only.";
                    validation.ErrorTitle = "Invalid Decimal";
                    validation.ErrorMessage = "Only decimal values allowed.";
                    validation.ShowErrorMessage = true;
                    break;
                case ExcelColumnType.Date:
                    validation.Date.Between(new DateTime(2000, 1, 1), new DateTime(2100, 12, 31));
                    validation.InputTitle = "Date Only";
                    validation.InputMessage = "Enter valid date.";
                    validation.ErrorTitle = "Invalid Date";
                    validation.ErrorMessage = "Only valid date allowed.";
                    validation.ShowErrorMessage = true;
                    break;

                case ExcelColumnType.Dropdown: 
                    if (item.DropdownValues != null &&item.DropdownValues.Any())
                    {
                        string dropdownColumn =GetExcelColumnName(columnNumber);
                        int totalItems =item.DropdownValues.Count;

                        string formula =$"=MasterData!${dropdownColumn}$1:${dropdownColumn}${totalItems}";
                        validation.List(formula, true);
                    }
                    validation.ErrorTitle = "Invalid Value";
                    validation.ErrorMessage ="Please select value from dropdown.";
                    validation.ShowErrorMessage = true;
                    break;

                case ExcelColumnType.Boolean:
                    validation.List("TRUE,FALSE");
                    validation.ErrorTitle = "Invalid Value";
                    validation.ErrorMessage = "Only TRUE or FALSE allowed.";
                    validation.ShowErrorMessage = true;
                    break;

                default:
                    break;
            }
            validation.IgnoreBlanks = !item.IsRequired;
            validation.InCellDropdown = true;
        }
        private string GetExcelColumnName(int columnNumber)
        {
            int divedend = columnNumber;
            string columnName = string.Empty;
            while (divedend > 0)
            {
                int modulo = (divedend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                divedend = (divedend - modulo) / 26;
            }
            return columnName;
        }
    }
}
