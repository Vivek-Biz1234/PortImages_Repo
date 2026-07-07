namespace PORTIMAGES.Common.Responses
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(int status, string message, T data = default)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Success<T>(T data, string message = "Success")
           => new ApiResponse<T>(1, message, data);

        public static ApiResponse<T> AlreadyExists<T>(string message)
            => new ApiResponse<T>(2, message);

        public static ApiResponse<T> NotFound<T>(string message)
            => new ApiResponse<T>(-1, message);

        public static ApiResponse<T> Error<T>(string message)
            => new ApiResponse<T>(-99, message);
    }
}
