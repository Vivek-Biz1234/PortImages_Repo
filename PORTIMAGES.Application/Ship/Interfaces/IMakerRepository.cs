using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Ship.Interfaces
{
    public interface IMakerRepository
    {
        Task<ApiResponse<object>> AddMakerAsync(MakerRequestDTO dto);
        Task<ApiResponse<object>> UpdateMakerAsync(MakerRequestDTO dto);
        Task<ApiResponse<object>> DeleteMakerAsync(long id, int deletedBy);

        Task<ApiResponse<MakerRequestDTO?>> GetMakerByIdAsync(long id);
        Task<ApiResponse<List<MakerResponseDTO>>> GetMakerListAsync();
    }
}
