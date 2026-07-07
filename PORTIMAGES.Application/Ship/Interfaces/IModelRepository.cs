using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Ship.Interfaces
{
    public interface IModelRepository
    {
        Task<ApiResponse<object>> AddModelAsync(ModelRequestDTO request);
        Task<ApiResponse<object>> UpdateModelAsync(ModelRequestDTO request);
        Task<ApiResponse<object>> DeleteModelAsync(long id, int deletedBy);
        Task<ApiResponse<ModelRequestDTO?>> GetModelByIdAsync(long id);
        Task<ApiResponse<List<ModelResponseDTO>>> GetModelListAsync();
    }
}
