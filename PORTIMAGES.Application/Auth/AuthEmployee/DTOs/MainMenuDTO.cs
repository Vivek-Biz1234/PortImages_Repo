namespace PORTIMAGES.Application.Auth.AuthEmployee.DTOs
{
    public class MainMenuDTO
    {
        public int MainMenuId { get; set; }
        public string? MainMenuName { get; set; }
        public string? Icon { get; set; }
        public List<SubMenuDTO> SubMenus { get; set; } = new List<SubMenuDTO>();
    }

    public class SubMenuDTO
    {
        public int SubMenuId { get; set; }
        public int MainMenuId { get; set; }
        public string? SubMenuName { get; set; }
        public string? Icon { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
    }
}
