using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class AgentCommissionPayment
    {
        [Key]
        public int CommissionPaymentId { get; set; }


        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }


        public string PaymentReference { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string PaymentMode { get; set; }

        public string? TransactionReference { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
