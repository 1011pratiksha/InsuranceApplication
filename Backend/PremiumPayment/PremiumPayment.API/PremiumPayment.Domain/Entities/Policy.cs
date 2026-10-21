using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class policy
    {
        public long policy_id { get; set; }
        public string policy_number { get; set; } = string.Empty;
        public long quote_id { get; set; }
        public long customer_id { get; set; }
        public long product_id { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime effective_date { get; set; }
        public DateTime expiry_date { get; set; }
        public string policy_status { get; set; } = string.Empty;
    }
}

