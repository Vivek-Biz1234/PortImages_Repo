namespace PORTIMAGES.Application.Menu.DTOs
{
    public class MainMenuResponseDTO
    {
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public string? Icon { get; set; }

        public bool IsActive { get; set; }        // Optional: for future use

        public string CreatedBy { get; set; }     // Employee name
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }     // Employee name
        public string? UpdatedOn { get; set; }
    }
}
