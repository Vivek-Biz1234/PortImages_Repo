
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;

namespace PORTIMAGES.Application.Auth.AuthEmployee.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<LoginResultDTO?> LoginAsync(string userName, string password);
    }
}
