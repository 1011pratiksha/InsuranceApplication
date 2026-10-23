using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Constants
{
    public static class refund_status_values
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Completed = "Completed";
        public const string Rejected = "Rejected";

        public static readonly string[] All =
            { Pending, Approved, Completed, Rejected };
    }

}
