using PORTIMAGES.Application.BrokerConsignee.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.BrokerConsignee.Interfaces
{
    public interface IBrokerMasterRepository
    {
        Task<ApiResponse<object>> AddBrokerAsync(BrokerMasterRequestDTO request);
        Task<ApiResponse<object>> UpdateBrokerAsync(BrokerMasterRequestDTO request);
        Task<ApiResponse<object>> DeleteBrokerAsync(int id, int DeletedBy);
        Task<ApiResponse<BrokerMasterRequestDTO?>> GetBrokerByIdAsync(int id);
        Task<ApiResponse<List<BrokerMasterResponseDTO>>> GetBrokerListAsync();
    }
}
