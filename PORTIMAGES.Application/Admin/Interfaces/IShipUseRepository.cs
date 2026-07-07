using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IShipUseRepository
    {
        Task<ApiResponse<object>> AddShipUseAsync(ShipUseRequestDTO request);

        Task<ApiResponse<List<ShipUseResponseDTO>>> GetShipUseStatusListAsync();


        Task<ApiResponse<ShipUseRequestDTO>> GetShipUseStatusByIdAsync(int id);


        Task<ApiResponse<object>> UpdateShipUseStatusStatusAsync(ShipUseRequestDTO request);

        Task<ApiResponse<object>> DeleteShipUseStatusAsync(int id, int DeletedBy);
    }
}
