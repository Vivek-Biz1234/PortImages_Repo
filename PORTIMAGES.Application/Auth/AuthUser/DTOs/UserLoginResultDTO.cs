namespace PORTIMAGES.Application.Auth.AuthUser.DTOs
{
    public class UserLoginResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public int? UserID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Action { get; set; }
        public string? Controller { get; set; }

        public string RoleName { get; set; } = "User";
        public string Scheme { get; set; } = "UserScheme";
    }
}
