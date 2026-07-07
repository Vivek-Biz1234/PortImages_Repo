using MediatR;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Application.User.Queries;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.User.Handlers
{
    public class GetInquiryByIdQueryHandler : IRequestHandler<GetInquiryByIdQuery, ApiResponse<InquiryRequestDTO?>>
    {
        private readonly IInquiryRepository _inquiryRepository;
        public GetInquiryByIdQueryHandler(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }
        public async Task<ApiResponse<InquiryRequestDTO?>> Handle(GetInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _inquiryRepository.GetInquiryByIdAsync(request.Id);
        }
    }
}
