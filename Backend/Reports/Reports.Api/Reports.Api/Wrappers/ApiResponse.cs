namespace Reports.Api.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Message = string.Empty;
            Timestamp = DateTime.UtcNow;
        }

        public ApiResponse(
            bool success,
            string message,
            T? data,
            string? error = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Error = error;
            Timestamp = DateTime.UtcNow;
        }
    }
}
