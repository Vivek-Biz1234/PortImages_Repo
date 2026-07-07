using System.Text.Json.Serialization;

namespace PORTIMAGES.Application.Menu.DTOs
{
    public class EmployeeInfoDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
