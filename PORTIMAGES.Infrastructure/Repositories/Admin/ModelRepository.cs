using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ModelRepository : IModelRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ModelRepository> _logger;

        public ModelRepository(
            IDapperRepository dapper,
            ILogger<ModelRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        #region ADD
        public async Task<ApiResponse<object>> AddModelAsync(ModelRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@CategoryId", request.CategoryId);
                param.Add("@MakerId", request.MakerId);
                param.Add("@ModelName", request.ModelName);
                param.Add("@Title", request.Title);
                param.Add("@Keyword", request.Keyword);
                param.Add("@Description", request.Description);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);

                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync(
                    "dbo.usp_add_models",
                    param,
                    CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Model added successfully !!"),
                    2 => new ApiResponse<object>(2, "Model name already exists !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                _logger.LogError(ex, "AddModel failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region UPDATE
        public async Task<ApiResponse<object>> UpdateModelAsync(ModelRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@ID", request.ID);
                param.Add("@CategoryId", request.CategoryId);
                param.Add("@MakerId", request.MakerId);
                param.Add("@ModelName", request.ModelName);
                param.Add("@Title", request.Title);
                param.Add("@Keyword", request.Keyword);
                param.Add("@Description", request.Description);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);

                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync(
                    "dbo.usp_update_models",
                    param,
                    CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Model updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Model name already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Model not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                _logger.LogError(ex, "UpdateModel failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region DELETE
        public async Task<ApiResponse<object>> DeleteModelAsync(long id, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@ID", id);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync(
                    "dbo.usp_delete_models",
                    param,
                    CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Model deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Model not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                _logger.LogError(ex, "DeleteModel failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region GET BY ID
        public async Task<ApiResponse<ModelRequestDTO?>> GetModelByIdAsync(long id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ModelRequestDTO>(
                    "dbo.usp_get_models_by_id",
                    new { ID = id },
                    CommandType.StoredProcedure);

                if (data == null)
                {
                    return new ApiResponse<ModelRequestDTO?>(
                        -1,
                        "Model not found !!",
                        null);
                }

                return new ApiResponse<ModelRequestDTO?>(
                    1,
                    "Success",
                    data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                _logger.LogError(ex, "GetModelById failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<ModelRequestDTO?>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region LIST
        public async Task<ApiResponse<List<ModelResponseDTO>>> GetModelListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ModelResponseDTO>(
                    "dbo.usp_get_models_list",
                    null,
                    CommandType.StoredProcedure);

                return new ApiResponse<List<ModelResponseDTO>>(
                    1,
                    "Success",
                    data.ToList());
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                _logger.LogError(ex, "GetModelList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<ModelResponseDTO>>(
                    -99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion
    }
}
