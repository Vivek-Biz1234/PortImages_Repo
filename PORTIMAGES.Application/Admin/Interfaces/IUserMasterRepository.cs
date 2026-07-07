using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IUserMasterRepository
    {
        Task<ApiResponse<object>> AddCompanyAsync(UserRequestDTO request);
        Task<ApiResponse<object>> UpdateCompanyAsync(UserRequestDTO request);
        Task<ApiResponse<object>> DeleteCompanyAsync(int id, int DeletedBy);
        Task<ApiResponse<UserRequestDTO?>> GetCompanyByIdAsync(int id);
        Task<ApiResponse<List<UserResponseDTO>>> GetCompanyListAsync();
    }
}
