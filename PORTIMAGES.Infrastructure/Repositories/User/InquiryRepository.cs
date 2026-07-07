using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data; 

namespace PORTIMAGES.Infrastructure.Repositories.Auth.User
{
    public class InquiryRepository:IInquiryRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<InquiryRepository> _logger;

        public InquiryRepository(IDapperRepository dapper, ILogger<InquiryRepository> logger)
        {
            this._dapper = dapper;
            this._logger = logger;
        }
          
        public async Task<ApiResponse<object>> AddInquiryAsync(InquiryRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ClientID", request.ClientID);
                param.Add("@MobileNo", request.MobileNo);
                param.Add("@Description", request.Description);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await _dapper.ExecuteAsync("USP_Add_Inquiry", param, CommandType.StoredProcedure);
                short result = param.Get<short?>("@Status") ?? -99; // 1, 2, -99
                return result switch
                {
                    1 => new ApiResponse<object>(1, "Inquiry added successfully !!"),
                    2 => new ApiResponse<object>(2, "Inquiry already exists !!"),
                    _ => new ApiResponse<object>(3, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "AddInquiry failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }

        public async Task<ApiResponse<InquiryRequestDTO?>> GetInquiryByIdAsync(int id)
        {
            try
            {
                
                var data = await _dapper.QueryFirstOrDefaultAsync<InquiryRequestDTO>("USP_Get_InquiryById", new { ID = id }, CommandType.StoredProcedure);
                if (data == null)
                {
                    return new ApiResponse<InquiryRequestDTO?>(-1, "Inquiry not found !!", null);
                }
                return new ApiResponse<InquiryRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8); // first 8 chars              
                //_logger.LogError(ex, "GetInquiryById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<InquiryRequestDTO?>(-99, "Something went wrong.<br/>Please contact to support with Error ID: " + errorId);
            }
        }
    }
}
