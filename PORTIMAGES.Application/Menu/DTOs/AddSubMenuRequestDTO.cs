
namespace PORTIMAGES.Application.Menu.DTOs
{
    public class AddSubMenuRequestDTO
    {
        public int SubMenuId { get; set; } = 0; // 0 for Add, >0 for Update
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int? ShortOrder { get; set; }
        public bool IsActive { get; set; } = true; 
        public int CreatedBy { get; set; } = 0;
        public int UpdatedBy { get; set; } = 0;
    }
}
