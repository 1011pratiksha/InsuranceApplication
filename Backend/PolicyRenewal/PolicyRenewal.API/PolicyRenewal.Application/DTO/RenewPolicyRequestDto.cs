using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.DTO
{
    public class RenewPolicyRequestDto
    {
        public DateTime CurrentExpiryDate { get; set; }
        public int RenewalTermInYears { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsPremiumPaid { get; set; }
    }
}
