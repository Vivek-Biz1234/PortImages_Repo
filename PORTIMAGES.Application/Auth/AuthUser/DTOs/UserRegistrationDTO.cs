using System.ComponentModel.DataAnnotations;

namespace PORTIMAGES.Application.Auth.AuthUser.DTOs
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^(?:\+91|0)?[6-9]\d{9}$",ErrorMessage = "Please enter a valid mobile number")]
        public string Mobile { get; set; }
         
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression(
        //    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@@$!%*?&]).{8,20}$",
        //    ErrorMessage = "Password must contain uppercase, lowercase, number and special character"
        //)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
