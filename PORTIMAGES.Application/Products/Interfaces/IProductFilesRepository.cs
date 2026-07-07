using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Products.Interfaces
{
    public interface IProductFilesRepository
    {
        Task<ApiResponse<object>> AddProductImageAsync(UploadProductImageRequesDTO request);
        Task<ApiResponse<List<ProductImageResponseDTO>>> GetProductImagesAsync(long productId); 
        Task<ApiResponse<object>> DeleteProductImageAsync(long id, int DeletedBy);
        Task<ApiResponse<List<ProductInnerDetailsDTO>>> GetProductInnerDetailsAsync(long productId);
        Task<ApiResponse<object>> DeleteProductComponentDetailsAsync(long productID, int deletedBy);
    }
}
