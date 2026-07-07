using MediatR;
using PORTIMAGES.Application.Auth.AuthUser.DTOs;
namespace PORTIMAGES.Application.Auth.AuthUser.Commands
{
    public class UserLoginCommand : IRequest<UserLoginResultDTO>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
