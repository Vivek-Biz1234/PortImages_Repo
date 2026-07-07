namespace PORTIMAGES.Application.Menu.DTOs
{
    public class SubMenuResponseDTO
    {
        public int SubMenuId { get; set; }
        public int MainMenuId { get; set; }
        public string? MainMenuName { get; set; } // From join with MainMenus
        public string SubMenuName { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int? ShortOrder { get; set; }
        public bool IsActive { get; set; } 
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
