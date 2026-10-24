using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class PolicyRenewal
    {
        [Key]
        public long renewal_id { get; set; }
        //[ForeignKey("Policy")]
        public long policy_id { get; set; }
        //public  Policy? Policy { get; set; }
        public DateTime renewal_date { get; set; }
        public DateTime new_expiry_date { get; set; }
        public decimal premium_amount { get; set; }
        public string renewal_status { get; set; } 
    }
}
