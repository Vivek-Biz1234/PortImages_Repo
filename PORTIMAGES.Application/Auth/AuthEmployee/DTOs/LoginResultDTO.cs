namespace PORTIMAGES.Application.Auth.AuthEmployee.DTOs
{
    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        // Fields from DB
        public int? ID { get; set; }
        public string? Name { get; set; } 
        public string? Email { get; set; } 
        public string? Action { get; set; }
        public string? Controller { get; set; }
        public string? RoleName { get; set; }
        public string? Profile { get; set; }

        public string Scheme { get; set; } = string.Empty;
    }
}
