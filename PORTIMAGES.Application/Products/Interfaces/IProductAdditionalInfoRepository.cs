using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Products.Interfaces
{
    public interface IProductAdditionalInfoRepository
    {
        Task<ApiResponse<List<FoundCarResponseDTO>>> GetFoundCarsAsync(FoundCarRequestDTO req);
        Task<ApiResponse<object>> AssignTerminalAsync(CommonAssignmentRequestDTO request);
        Task<ApiResponse<object>> MarkProductFoundOrNotFoundAsync(CommonAssignmentRequestDTO request);
    }
}
