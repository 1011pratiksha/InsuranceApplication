using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.DTO
{
    public class PolicyDto
    {
        public int policy_id { get; set; }
        public string policy_number { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime effective_date { get; set; }
        public DateTime expiry_date { get; set; }
        public string policy_status { get; set; }
    }
}
