using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class RoleMaster
    {
        public long role_id { get; set; }
        public string role_name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
    }
}
