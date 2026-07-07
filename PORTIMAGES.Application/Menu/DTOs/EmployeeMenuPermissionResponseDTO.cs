namespace PORTIMAGES.Application.Menu.DTOs
{
    public class EmployeeMenuPermissionResponseDTO
    {
        public EmployeeInfoDTO Employee { get; set; } = new();
        public List<EmployeeMenuPermissionDTO> Menus { get; set; } = new();
    }
}
