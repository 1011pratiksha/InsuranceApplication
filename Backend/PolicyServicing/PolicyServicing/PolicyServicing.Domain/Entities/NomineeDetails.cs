using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class NomineeDetails
    {
        [Key]
        public long nominee_id { get; set; }

        [ForeignKey("Policy")]
        public long policy_id { get; set; }
        public  Policy? Policy { get; set; }
        public string nominee_name { get; set; } 
        public string relationship { get; set; } 
        public DateTime dob { get; set; }
        public decimal share_percentage { get; set; }
        public string? guardian_name { get; set; }
    }
}
