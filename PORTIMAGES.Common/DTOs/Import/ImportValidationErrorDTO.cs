namespace PORTIMAGES.Common.DTOs.Import
{
    public class ImportValidationErrorDTO
    {
        public int RowNumber { get; set; } 
        public string ColumnName { get; set; } = string.Empty; 
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
