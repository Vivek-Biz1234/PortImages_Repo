using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IInventoryStatusRepository
    {
        Task<ApiResponse<object>> AddInventoryStatusAsync(InventoryStatusRequestDTO request);
        Task<ApiResponse<object>> UpdateInventoryStatusAsync(InventoryStatusRequestDTO request);
        Task<ApiResponse<object>> DeleteInventoryStatusAsync(int id,int DeletedBy);
        Task<ApiResponse<InventoryStatusRequestDTO>> GetInventoryStatusByIdAsync(int id);
        Task<ApiResponse<List<InventoryStatusResponseDTO>>> GetInventoryStatusListAsync();
    }
}
