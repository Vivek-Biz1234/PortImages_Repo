using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.User.Interfaces
{
    public interface IInquiryRepository
    {
        Task<ApiResponse<object>> AddInquiryAsync(InquiryRequestDTO request);

        Task<ApiResponse<InquiryRequestDTO?>> GetInquiryByIdAsync(int id);
    }
}
