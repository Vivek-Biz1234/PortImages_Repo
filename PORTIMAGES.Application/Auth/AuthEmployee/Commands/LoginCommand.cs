using MediatR;
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs; 

namespace PORTIMAGES.Application.Auth.AuthEmployee.Commands
{
    public class LoginCommand:IRequest<LoginResultDTO>
    {         
        public string Username { get; set; } = string.Empty;  
        public string Password { get; set; } = string.Empty;
    }
}
