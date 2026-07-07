using MediatR;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Application.User.Commands;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Auth.AuthUser.Handlers
{
    public class AddInquiryCommondHandler:IRequestHandler<AddInquiryCommand, ApiResponse<object>>
    {
        private readonly IInquiryRepository _inquiryRepository;
        public AddInquiryCommondHandler(IInquiryRepository inquiryRepository)
        {
            this._inquiryRepository = inquiryRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddInquiryCommand request, CancellationToken cancellationToken)
        {
            var dto = new InquiryRequestDTO
            {
                ClientID = request.ClientID,
                MobileNo = request.MobileNo,
                Description = request.Description,
                IsActive = request.IsActive??false,
                CreatedBy = request.CreatedBy
            };
            return await _inquiryRepository.AddInquiryAsync(dto);
        } 
    }
}
