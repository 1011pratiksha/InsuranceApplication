using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Domain.Entities
{
    public class PolicyRenewal
    {
        public int renewal_id { get; set; }
        public int policy_id { get; set; }
        public DateTime renewal_date { get; set; }
        public DateTime new_expiry_date { get; set; }
        public decimal premium_amount { get; set; }
        public string renewal_status { get; set; }
    }
}
