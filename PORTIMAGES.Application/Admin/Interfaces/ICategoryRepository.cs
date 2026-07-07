using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ApiResponse<object>> AddCategoryAsync(CategoryRequestDTO request); 
        Task<ApiResponse<List<CategoryResponseDTO>>> GetCategoryListAsync(); 
        Task<ApiResponse<CategoryRequestDTO?>> GetCategoryByID(int id); 
        Task<ApiResponse<object>> DeleteCategoryAsync(int id, int DeletedBy);
        Task<ApiResponse<object>> UpdateCategoryAsync(CategoryRequestDTO request);
    }
}
