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


        public int PolicyId { get; set; }



        public int CustomerId { get; set; }


        public DateTime AssignedDate { get; set; }

        public DateTime? SaleDate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
