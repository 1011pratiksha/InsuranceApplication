using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.DTO
{
    public class CommissionResponseDto
    {
        public int CommissionId { get; set; }

        public int AgentId { get; set; }

        public int PolicyId { get; set; }

        public decimal PremiumAmount { get; set; }

        public decimal CommissionPercentage { get; set; }

        public decimal CommissionAmount { get; set; }

        public string Status { get; set; }

        public DateTime EarnedDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
