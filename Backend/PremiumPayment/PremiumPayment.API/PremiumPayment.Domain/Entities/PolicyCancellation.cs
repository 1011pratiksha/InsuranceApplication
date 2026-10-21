using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class PolicyCancellation
    {
        public long cancellation_id { get; set; }
        public long policy_id { get; set; }
        public DateTime cancellation_date { get; set; }
        public string? cancellation_reason { get; set; }
        public decimal refund_amount { get; set; }
        public long approved_by { get; set; }
    }
}

