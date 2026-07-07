using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Ship.Interfaces
{
    public interface IShipRepository
    {
        Task<ApiResponse<object>> AddShipAsync(ShipRequestDTO dto);
        Task<ApiResponse<object>> UpdateShipAsync(ShipRequestDTO dto);
        Task<ApiResponse<object>> DeleteShipAsync(int id, int deletedBy);
        Task <ApiResponse<ShipRequestDTO?>> GetShipByIdAsync(int id);
        Task <ApiResponse<List<ShipResponseDTO>>> GetShipListAsync();
    }
}
