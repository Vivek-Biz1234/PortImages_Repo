namespace PORTIMAGES.Application.Menu.DTOs
{
    public class EmployeeMenuPermissionDTO
    {
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }

        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }

        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public bool IsAssigned { get; set; }
    }
}
