namespace PORTIMAGES.Application.Menu.DTOs
{
    public class SaveEmployeeMenuDTO
    {
        public int EmpId { get; set; }
        public int SubMenuId { get; set; } 
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public int CreatedBy { get; set; }        
    }
}
