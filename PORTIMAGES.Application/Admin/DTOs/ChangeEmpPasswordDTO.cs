namespace PORTIMAGES.Application.Admin.DTOs
{
    public class ChangeEmpPasswordDTO
    {
        public int ID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
