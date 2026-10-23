using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.DTO
{
    public class RenewalDto2
    {
        public long renewal_id { get; set; }
        public long policy_id { get; set; }
        public DateTime renewal_date { get; set; }
        public DateTime new_expiry_date { get; set; }
        public decimal premium_amount { get; set; }
        public string renewal_status { get; set; } 
    }
}
