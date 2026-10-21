using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.DTO
{
    public class PolicyRenewalResponseDto
    {
        public int RenewalId { get; set; }
        public int PolicyId { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime NewExpiryDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public string RenewalStatus { get; set; }
    }
}
