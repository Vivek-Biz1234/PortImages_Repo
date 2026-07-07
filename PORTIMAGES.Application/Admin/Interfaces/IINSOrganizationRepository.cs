using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface  IINSOrganizationRepository
    {
        Task<ApiResponse<object>> AddINSOrganizationAsync(INSOrganizationRequestDTO request);

        Task<ApiResponse<List<INSOrganizationResponseDTO>>> GetINSOrganizationListAsync();

        Task<ApiResponse<INSOrganizationRequestDTO>> GetINSOrganizationStatusByIdAsync(int id);

        Task<ApiResponse<object>> UpdateINSOrganizationAsync(INSOrganizationRequestDTO request);

        Task<ApiResponse<object>> DeleteINSOrganizationAsync(int id, int DeletedBy);
    }
}
