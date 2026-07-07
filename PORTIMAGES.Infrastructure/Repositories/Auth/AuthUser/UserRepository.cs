using Dapper;
using Microsoft.Extensions.Logging; 
using PORTIMAGES.Application.Auth.AuthUser.DTOs;
using PORTIMAGES.Application.Auth.AuthUser.Interfaces;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data;
namespace PORTIMAGES.Infrastructure.Repositories.Auth.AuthUser
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(IDapperRepository dapper, ILogger<UserRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        } 
        public async Task<UserLoginResultDTO> LoginAsync(string username,string password)
        {
            try
            {
                //string decryptedValue = CryptoHelper.Decrypt("388cw6sZc5qgjQuxIYMdJA");
                string passwordHash = CryptoHelper.Encrypt(password);
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", passwordHash);
                return await _dapper.QueryFirstOrDefaultAsync<UserLoginResultDTO>("dbo.usp_auth_user", parameters, CommandType.StoredProcedure,300);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "User LoginAsync failed | ErrorId: {ErrorId}", errorId);
                return new UserLoginResultDTO
                {
                    Success = false,
                    Message = "Something went wrong.\n Please contact to support with Error ID:" + errorId
                };
            }
        }
    }
}
