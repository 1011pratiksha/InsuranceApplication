using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class Agent
    {
        [Key]
        public int AgentId { get; set; }


        //[ForeignKey("UserId")]
        public int UserId { get; set; }
        //public User User { get; set; }



        public string AgentCode { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; } 

        public string MobileNo { get; set; } 

        public DateTime JoiningDate { get; set; }

        public string? LicenseNumber { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
