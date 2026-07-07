using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;

namespace PORTIMAGES.Application.Auth.AuthEmployee.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MainMenuDTO>> GetMenuByUserIdAsync(int empId);
    }
}
