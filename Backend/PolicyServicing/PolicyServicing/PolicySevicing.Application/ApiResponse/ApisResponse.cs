using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.ApiResponse
{
    public class ApisResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public ApisResponse(bool success, string message, T? data, string error = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Error = error;
        }
        public static ApisResponse<T> SuccessResponse(T data, string message = "Request Success")
        {
            return new ApisResponse<T>(true, message, data);
        }

        public static ApisResponse<T> ErrorResponse(string message = "Request Failed", string error = null)
        {
            return new ApisResponse<T>(false, message, default, error);
        }
    }
}
