using PORTIMAGES.Application.Common.Interfaces;
using PORTIMAGES.Common.DTOs;
using PORTIMAGES.Infrastructure.Persistence; 
using System.Data; 

namespace PORTIMAGES.Infrastructure.Common.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly IDapperRepository _dapper; 
        public ErrorLogRepository(IDapperRepository dapper)
        {
            _dapper = dapper;
        } 
        public async Task LogAsync(ErrorLog errorLog)
        {
            await _dapper.ExecuteAsync(
                "dbo.usp_add_errorlog",
                new
                {
                    ErrorId = errorLog.ErrorId,
                    ControllerName = errorLog.ControllerName,
                    ActionName = errorLog.ActionName,
                    FileName = errorLog.FileName,
                    LineNumber = errorLog.LineNumber,
                    StoredProcedure = errorLog.StoredProcedure,
                    ErrorMessage = errorLog.ErrorMessage,
                    StackTrace = errorLog.StackTrace
                },
                CommandType.StoredProcedure
            );

        } 
        public async Task<ErrorLog?> GetByErrorIdAsync(string errorId)
        {
            return await _dapper.QueryFirstOrDefaultAsync<ErrorLog>(
                "dbo.usp_get_errorlog_by_errorid",
                new { ErrorId = errorId },
                CommandType.StoredProcedure
            );
        }
    }
}
