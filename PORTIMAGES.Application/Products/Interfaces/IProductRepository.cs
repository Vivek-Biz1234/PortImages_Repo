using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Common.Responses;
using System.Data;

namespace PORTIMAGES.Application.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<ApiResponse<object>> AddProductAsync(AddProductRequestDTO dto);

        Task<ApiResponse<object>> UpdateProductAsync(AddProductRequestDTO dto);

        Task<ApiResponse<object>> DeleteProductAsync(long id, int deletedBy);

        Task<ApiResponse<AddProductRequestDTO?>> GetProductByIdAsync(long id);

        Task<ApiResponse<List<ProductResponseDTO>>> GetProductListAsync(ViewProductsRequestDTO req);        

        Task<ApiResponse<List<ProductConsigneeResponseDTO>>> GetProductConsigneeListAsync(ProductConsigneeRequestDTO req);
        Task<ApiResponse<List<ProductConsigneeResponseDTO>>> GetProductConsigneeByIdAsync(int ProductId);

        Task<ApiResponse<object>> AssignConsigneeAsync(AssignProdConsigneeRequestDTO request);
        Task<ApiResponse<object>> AssignConsigneeAsync_Old(AssignProdConsigneeRequestDTO_Old request);
        Task<ApiResponse<List<ProductBrokerResponseDTO>>> GetProductBrokerListAsync(ProductBrokerRequestDTO req);
        Task<ApiResponse<object>> AssignBrokerAsync(AssignProdBrokerRequestDTO request);
        Task<ApiResponse<object>> ImportProductViaFileAsync(DataTable dt);
        Task<List<string>> GetExistingChassisViaFileAsync(DataTable dt);
    }
}
