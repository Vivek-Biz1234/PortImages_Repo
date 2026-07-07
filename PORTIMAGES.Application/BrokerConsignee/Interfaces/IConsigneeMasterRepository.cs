using PORTIMAGES.Application.BrokerConsignee.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.BrokerConsignee.Interfaces
{
    public interface IConsigneeMasterRepository
    {
        Task<ApiResponse<object>> AddConsigneeAsync(ConsigneeMasterRequestDTO request);
        Task<ApiResponse<object>> UpdateConsigneeAsync(ConsigneeMasterRequestDTO request);
        Task<ApiResponse<object>> DeleteConsigneeAsync(int id, int DeletedBy);
        Task<ApiResponse<ConsigneeMasterRequestDTO?>> GetConsigneeByIdAsync(int id);
        Task<ApiResponse<List<ConsigneeMasterResponseDTO>>> GetConsigneeListAsync();
    }
}
