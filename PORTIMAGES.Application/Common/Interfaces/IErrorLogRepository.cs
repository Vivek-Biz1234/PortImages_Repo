using PORTIMAGES.Common.DTOs;

namespace PORTIMAGES.Application.Common.Interfaces
{
    public interface IErrorLogRepository
    {
        Task LogAsync(ErrorLog errorLog);
        Task<ErrorLog?> GetByErrorIdAsync(string errorId);
    }
}
