using MediatR;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Application.User.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.User.Handler
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ApiResponse<ViewProductDetailsDTO>>
    {
        private readonly IUserProductRepository _repository;
        public GetProductDetailsByIdQueryHandler(IUserProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<ViewProductDetailsDTO>> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetProductDetailsByIdAsync(request.ProductId,request.ClientId);
        }
    }
}
