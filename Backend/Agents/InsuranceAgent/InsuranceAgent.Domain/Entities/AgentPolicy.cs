using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class AgentPolicy
    {
        [Key]
        public int AgentPolicyId { get; set; }


        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }


        [ForeignKey("PolicyId")]
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }



        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public DateTime AssignedDate { get; set; }

        public DateTime? SaleDate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
