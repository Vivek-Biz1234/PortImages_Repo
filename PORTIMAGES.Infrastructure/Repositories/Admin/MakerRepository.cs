using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class MakerRepository : IMakerRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<MakerRepository> _logger;
        private readonly FileHelper _fileHelper;
        string allowedExtensions = ".jpg,.png,.jpeg,.webp";

        public MakerRepository(IDapperRepository dapper, ILogger<MakerRepository> logger, FileHelper fileHelper)
        {
            _dapper = dapper;
            _logger = logger;
            _fileHelper = fileHelper;
        }
        
        #region Add
        public async Task<ApiResponse<object>> AddMakerAsync(MakerRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                string? logoPath = null;

                if (request.Logo != null && request.Logo.Length > 0)
                {
                    logoPath = await _fileHelper.SaveFileAsync(request.Logo, "MakerLogos", allowedExtensions);
                }
                else
                {
                    // keep existing logo from DB
                    logoPath = request.Logo_SRC;
                }

                param.Add("@CountryId", request.CountryId);
                param.Add("@MakerName", request.MakerName);
                param.Add("@Title", request.Title);
                param.Add("@Keyword", request.Keyword);
                param.Add("@Description", request.Description);
                param.Add("@Canonical", request.Canonical);
                param.Add("@Details", request.Details);
                param.Add("@Logo", logoPath);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);

                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("dbo.usp_add_maker",param,CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Maker added successfully !!"),
                    2 => new ApiResponse<object>(2, "Maker name already exists !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "AddMaker failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region Update
        public async Task<ApiResponse<object>> UpdateMakerAsync(MakerRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                string? LOGO_SRC = string.Empty;
                var req = await GetMakerByIdAsync(request.ID);
                if (req != null)
                {
                    LOGO_SRC = req.Data.Logo_SRC;
                    if (!string.IsNullOrEmpty(LOGO_SRC) &&(request.Logo != null && request.Logo.Length > 0))
                    {
                        _fileHelper.DeleteFile(LOGO_SRC);
                    }

                }
                string? logoPath = null;

                if (request.Logo != null && request.Logo.Length > 0)
                {
                    logoPath = await _fileHelper.SaveFileAsync(request.Logo, "MakerLogos", allowedExtensions);
                }
                else
                {
                    // keep existing logo from DB
                    logoPath = request.Logo_SRC;
                }

                param.Add("@ID", request.ID);
                param.Add("@CountryId", request.CountryId);
                param.Add("@MakerName", request.MakerName);
                param.Add("@Title", request.Title);
                param.Add("@Keyword", request.Keyword);
                param.Add("@Description", request.Description);
                param.Add("@Canonical", request.Canonical);
                param.Add("@Details", request.Details);
                param.Add("@Logo", logoPath);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);

                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_update_maker",param,CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Maker updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Maker name already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Maker not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "UpdateMaker failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region Delete
        public async Task<ApiResponse<object>> DeleteMakerAsync(long id, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                string? LOGO_SRC= string.Empty;
                param.Add("@ID", id);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                var req = await GetMakerByIdAsync(id);
                if (req != null)
                {
                    LOGO_SRC = req.Data.Logo_SRC;           
                    if(!string.IsNullOrEmpty(LOGO_SRC))
                    {
                        _fileHelper.DeleteFile(LOGO_SRC);
                    }

                } 
                await _dapper.ExecuteAsync("dbo.usp_delete_maker",param,CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Maker deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Maker not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "DeleteMaker failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<object>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region GetById
        public async Task<ApiResponse<MakerRequestDTO?>> GetMakerByIdAsync(long id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<MakerRequestDTO>("dbo.usp_get_maker_by_id",new { ID = id },CommandType.StoredProcedure);

                if (data == null)
                {
                    return new ApiResponse<MakerRequestDTO?>(-1, "Maker not found !!", null);
                }

                return new ApiResponse<MakerRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "GetMakerById failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<MakerRequestDTO?>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region List
        public async Task<ApiResponse<List<MakerResponseDTO>>> GetMakerListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<MakerResponseDTO>("dbo.usp_get_maker_list",null,CommandType.StoredProcedure);

                return new ApiResponse<List<MakerResponseDTO>>(1,"Success",data.ToList());
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "GetMakerList failed | ErrorId: {ErrorId}", errorId);

                return new ApiResponse<List<MakerResponseDTO>>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion
    }
}
