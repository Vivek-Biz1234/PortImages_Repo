using Azure.Core;
using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Auth.AuthEmployee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(IDapperRepository dapper, ILogger<EmployeeRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }
        public async Task<LoginResultDTO> LoginAsync(string username, string password)
        {

            try
            {
                string passwordHash = CryptoHelper.Encrypt(password);
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", passwordHash);

                var result = await _dapper.QueryFirstOrDefaultAsync<LoginResultDTO>("dbo.usp_auth_employee", parameters, CommandType.StoredProcedure,300);
                return result;
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                _logger.LogError(ex, "Emp LoginAsync failed | ErrorId: {ErrorId}", errorId);
                return new LoginResultDTO
                {
                    Success=false,
                    Message= "Something went wrong.\n Please contact to support with Error ID:" + errorId
                };
            } 
        }
    }
}
