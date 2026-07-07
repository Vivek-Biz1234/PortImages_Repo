using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(IDapperRepository Dapper, ILogger<CategoryRepository> logger)
        {
            this._dapper = Dapper;
            this._logger = logger;
        }

        public async Task<ApiResponse<object>> AddCategoryAsync(CategoryRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();               
                param.Add("@CategoryName", request.CategoryName);
                param.Add("@TitleTag", request.Titletag);
                param.Add("@Keyword", request.KeywordTag);
                param.Add("@Description", request.Description);                
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_Add_category", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Category added successfully !!"),
                    2 => new ApiResponse<object>(2, "Category already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "AddSubcategory failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> DeleteCategoryAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_DeleteCategory", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Category deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Category not found !!"),
                    _ => new ApiResponse<object>(-99, "Somthing went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "Delete Category failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<CategoryRequestDTO?>> GetCategoryByID(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<CategoryRequestDTO>("USP_Get_categoryById", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<CategoryRequestDTO?>(-1, "Category not found !!", null);
                }
                return new ApiResponse<CategoryRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetCategory failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<CategoryRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<List<CategoryResponseDTO>>> GetCategoryListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<CategoryResponseDTO>("USP_Get_category_List", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<CategoryResponseDTO>>(1, "Success", list);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "Get Category failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<CategoryResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateCategoryAsync(CategoryRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@CategoryName", request.CategoryName);
                param.Add("@Title", request.Titletag);
                param.Add("@Keyword", request.KeywordTag);
                param.Add("@Description", request.Description);                
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_UpdateCategory", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Category Updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Category already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Category not found !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "Category failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
