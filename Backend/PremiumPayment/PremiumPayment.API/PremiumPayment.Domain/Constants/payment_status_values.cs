using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Constants
{
    public static class payment_status_values
    {
        public const string Initiated = "Initiated";
        public const string Success = "Success";
        public const string Failed = "Failed";
        public const string Pending = "Pending";
        public const string Refunded = "Refunded";
        public const string Cancelled = "Cancelled";

        public static readonly string[] All =
            { Initiated, Success, Failed, Pending, Refunded, Cancelled };
    }

}
