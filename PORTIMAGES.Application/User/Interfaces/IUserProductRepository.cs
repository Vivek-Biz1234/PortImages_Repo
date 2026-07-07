using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.User.Interfaces
{
    public interface  IUserProductRepository
    {
        Task<ApiResponse<List<MyProductResponseDTOs>>> GetProductListAsync(MyProductRequestDTOs request);

        Task<ApiResponse<ViewProductDetailsDTO>> GetProductDetailsByIdAsync(long id,long clientId);
    }
}
