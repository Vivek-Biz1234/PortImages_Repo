namespace PORTIMAGES.Common.DTOs
{
    public class ErrorLog
    {
        public string ErrorId { get; set; } = null!; 
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? FileName { get; set; }
        public int? LineNumber { get; set; } 
        public string? StoredProcedure { get; set; } 
        public string ErrorMessage { get; set; } = null!;
        public string? StackTrace { get; set; } 
        public DateTime CreatedOn { get; set; }
    }
}
