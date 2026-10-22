using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class AgentCustomer
    {
        [Key]
        public int AgentCustomerId { get; set; }



        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }


        //[ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        //public Customer Customer { get; set; }


        public DateTime AssignedDate { get; set; }

        public int AssignedBy { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; }
    }
}
