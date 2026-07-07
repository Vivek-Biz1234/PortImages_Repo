using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IVehicleStatusRepository
    {
        Task<ApiResponse<object>> AddVehicleStatusAsync(VehicleStatusRequestDTO request);
        Task<ApiResponse<object>> UpdateVehicleStatusAsync(VehicleStatusRequestDTO request);
        Task<ApiResponse<object>> DeleteVehicleStatusAsync(int id, int DeletedBy);
        Task<ApiResponse<VehicleStatusRequestDTO>> GetVehicleStatusByIdAsync(int id);
        Task<ApiResponse<List<VehicleStatusResponseDTO>>> GetVehicleStatusListAsync();
    }
}
