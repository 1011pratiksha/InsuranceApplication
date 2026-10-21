using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Common
{
    /// <summary>Base for exceptions your global exception handler maps to HTTP status codes (BRD 1.15).</summary>
    public abstract class AppException : Exception
    {
        public int StatusCode { get; }
        protected AppException(string message, int statusCode, Exception? inner = null) : base(message, inner)
            => StatusCode = statusCode;
    }

    public sealed class BadRequestException(string message) : AppException(message, 400);
    public sealed class NotFoundException(string message) : AppException(message, 404);
    public sealed class BusinessRuleException(string message) : AppException(message, 422);
    public sealed class PaymentGatewayException(string message, Exception? inner = null) : AppException(message, 502, inner);

}
