using PORTIMAGES.Application.Auth.AuthUser.DTOs;

namespace PORTIMAGES.Application.Auth.AuthUser.Interfaces
{
    public interface IUserRepository
    {
        Task<UserLoginResultDTO> LoginAsync(string username, string password);
    }
}
