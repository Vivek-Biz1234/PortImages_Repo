using Microsoft.Extensions.Logging;

namespace PORTIMAGES.Common.Responses
{
    public static class ApiExceptionHandler
    {
        public static ApiResponse<T> Handle<T>(Exception ex,ILogger logger,string actionName)
        {
            var errorId = Guid.NewGuid().ToString()[..8];
            logger.LogError(ex,"{Action} failed | ErrorId: {ErrorId}", actionName,errorId);

            return ApiResponse<T>.Error<T>(
                $"Something went wrong.<br/>Please contact support with Error ID: {errorId}"
            );
        }
    }
}
