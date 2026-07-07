using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IINSDestinationRepository
    {
        Task<ApiResponse<object>> AddINSDestinationAsync(INSDestinationRequestDTO request);

        Task<ApiResponse<List<INSDestinationResponseDTO>>> GetINSDestionationListAsync();


        Task<ApiResponse<INSDestinationRequestDTO>> GetINSDestinationByIdAsync(int id);

        Task<ApiResponse<object>> UpdateINSDestinationAsync(INSDestinationRequestDTO request);

        Task<ApiResponse<object>> DeleteInventoryStatusAsync(int id, int DeletedBy);
    }
}
