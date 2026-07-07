namespace PORTIMAGES.Application.Menu.DTOs
{
    public class AddMainMenuRequestDTO
    {
        public int? MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
