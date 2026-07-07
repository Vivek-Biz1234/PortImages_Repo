using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IShipTypeRepository
    {
        Task<ApiResponse<object>> AddShipTypeAsync(ShipTypeRequestDTO request);

        Task<ApiResponse<List<ShipTypeResponseDTO>>> GetShipTypeListAsync();

        Task<ApiResponse<ShipTypeRequestDTO>> GetShipTypeStatusByIdAsync(int id);

        Task<ApiResponse<object>> UpdateShipTypeAsync(ShipTypeRequestDTO request);


        Task<ApiResponse<object>> DeleteShipTypeStatusAsync(int id, int DeletedBy);
    }
}
