using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.DTO
{
    public class RenewPolicyDto
    {
        public decimal premium_amount { get; set; }

        public DateTime new_expiry_date { get; set; }
    }
}
