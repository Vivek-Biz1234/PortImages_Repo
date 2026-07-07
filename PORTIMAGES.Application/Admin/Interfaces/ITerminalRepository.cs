using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface ITerminalRepository
    {
        Task<ApiResponse<object>> AddTerminalAsync(TerminalRequestDTO request);
        Task<ApiResponse<object>> UpdateTerminalAsync(TerminalRequestDTO request);
        Task<ApiResponse<object>> DeleteTerminalAsync(int id,int DeletedBy);
        Task<ApiResponse<TerminalRequestDTO?>> GetTerminalByIdAsync(int id);
        Task<ApiResponse<List<TerminalResponseDTO>>> GetTerminalListAsync();
    }
}
