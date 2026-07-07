using PORTIMAGES.Application.Ship.DTOs.PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Ship.Interfaces
{
    public interface IShippingRepository
    {
        Task<ApiResponse<object>> AddShippingAsync(ShippingRequestDTO dto);
        Task<ApiResponse<object>> UpdateShippingAsync(ShippingRequestDTO dto);
        Task<ApiResponse<object>> DeleteShippingAsync(int id, int deletedBy);
        Task<ApiResponse<ShippingRequestDTO?>> GetShippingByIdAsync(int id);
        Task<ApiResponse<List<ShippingResponseDTO>>> GetShippingListAsync();
    }
}
