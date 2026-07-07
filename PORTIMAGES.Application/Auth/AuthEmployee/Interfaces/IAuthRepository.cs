using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;

namespace PORTIMAGES.Application.Auth.AuthEmployee.Interfaces
{
    public interface IAuthRepository
    {
        Task SignInAsync(LoginResultDTO user);
        Task SignOutAsync(string Scheme);
    }
}
