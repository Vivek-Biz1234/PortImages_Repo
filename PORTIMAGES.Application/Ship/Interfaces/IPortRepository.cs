using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Ship.Interfaces
{
    public interface IPortRepository
    {
        Task<ApiResponse<object>> AddPortAsync(PortRequestDTO dto);
        Task<ApiResponse<object>> UpdatePortAsync(PortRequestDTO dto);
        Task<ApiResponse<object>> DeletePortAsync(int id, int deletedBy);
        Task<ApiResponse<PortRequestDTO?>> GetPortByIdAsync(int id);
        Task<ApiResponse<List<PortResponseDTO>>> GetPortListAsync();
    }
}
