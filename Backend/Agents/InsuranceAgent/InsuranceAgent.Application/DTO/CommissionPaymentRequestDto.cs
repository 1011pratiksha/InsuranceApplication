using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.DTO
{
    public class CommissionPaymentRequestDto
    {
        public int AgentId { get; set; }

        public string PaymentMode { get; set; }
    }
}
