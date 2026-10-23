using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.DTO
{
    public class CreateAgentPolicyDto
    {
        public int PolicyId { get; set; }

        public int CustomerId { get; set; }

        public DateTime? SaleDate { get; set; }
    }
}
