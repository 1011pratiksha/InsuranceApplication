using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsuranceAgent.Application.DTO
{
    public class CommissionPaymentResponseDto
    {
        public int CommissionPaymentId { get; set; }

        public int AgentId { get; set; }

        public string PaymentReference { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string PaymentMode { get; set; }

        public string? TransactionReference { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

