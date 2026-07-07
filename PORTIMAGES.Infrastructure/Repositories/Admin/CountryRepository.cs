using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data; 

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class CountryRepository:ICountryRepository   
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(IDapperRepository dapper, ILogger<CountryRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }

        public async Task<ApiResponse<object>> AddCountryAsync(CountryRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CountryName", request.CountryName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_AddCountry", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Country added successfully !!"),
                    2 => new ApiResponse<object>(2, "Country already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "AddTerminal failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> UpdateCountryAsync(CountryRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@CountryName", request.CountryName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_Update_Country", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Country updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Country already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Country not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "UpdateCountry failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<List<CountryResponseDTO>>> GetCountryListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<CountryResponseDTO>("USP_Get_Country_List", null, CommandType.StoredProcedure);
                var list = data.ToList();
                return new ApiResponse<List<CountryResponseDTO>>(1, "Success",  list);

            } 
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetCountryList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<CountryResponseDTO>>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<CountryRequestDTO?>> GetCountryByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<CountryRequestDTO>("dbo.USP_Get_Country_By_Id", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<CountryRequestDTO?>(-1, "Country not found !!", null);
                }
                return new ApiResponse<CountryRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetCountryById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<CountryRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<object>> DeleteCountryAsync(int id, int DeletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", DeletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_Delete_Country", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99;
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Country deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Country not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "DeleteCountry failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }

}
