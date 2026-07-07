using MediatR; 
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.User.Queries
{
    public  class GetInquiryByIdQuery : IRequest<ApiResponse<InquiryRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetInquiryByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
