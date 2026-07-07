using MediatR;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.User.Queries
{
    public class GetProductDetailsByIdQuery:IRequest<ApiResponse<ViewProductDetailsDTO>>
    {
        public long ProductId { get; set; }
        public long ClientId { get; set; }
    }
}
