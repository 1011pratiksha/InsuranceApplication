using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class AgentCommission
    {
        [Key]
        public int CommissionId { get; set; }


        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }


        [ForeignKey("PolicyId")]
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }


        public decimal PremiumAmount { get; set; }

        public decimal CommissionPercentage { get; set; }

        public decimal CommissionAmount { get; set; }

        public string Status { get; set; }

        public DateTime EarnedDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
