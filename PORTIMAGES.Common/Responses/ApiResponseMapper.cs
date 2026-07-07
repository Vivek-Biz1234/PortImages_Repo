using PORTIMAGES.Common.Enums;
using PORTIMAGES.Common.Helpers;

namespace PORTIMAGES.Common.Responses
{
    public static class ApiResponseMapper
    {
        public static ApiResponse<object> Map(ResultStatus status, string entity, CrudAction action)
        { 
            return status switch
            {
                ResultStatus.Success =>
                    ApiResponse<object>.Success<object>(
                        null,
                        MessageBuilder.Success(entity, action)
                    ),

                ResultStatus.EmailExists =>
                    ApiResponse<object>.AlreadyExists<object>(
                        MessageBuilder.EmailExists()
                    ),

                ResultStatus.MobileExists =>
                    ApiResponse<object>.AlreadyExists<object>(
                        MessageBuilder.MobileExists()
                    ),

                ResultStatus.NotFound =>
                    ApiResponse<object>.NotFound<object>(
                        MessageBuilder.NotFound(entity)
                    ),
                ResultStatus.Failed =>
                    ApiResponse<object>.NotFound<object>(
                        MessageBuilder.NotFound(entity)
                    ),

                _ =>
                    ApiResponse<object>.Error<object>(
                        MessageBuilder.Error()
                    )
            };
        }
    }
}
