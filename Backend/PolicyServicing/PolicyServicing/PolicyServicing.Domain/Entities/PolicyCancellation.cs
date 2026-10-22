using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class PolicyCancellation
    {
        [Key]
        public long cancellation_id { get; set; }

        [ForeignKey("Policy")]
        public long policy_id { get; set; }
        public  Policy? Policy { get; set; }
        public DateTime cancellation_date { get; set; }
        public string? cancellation_reason { get; set; }
        public decimal refund_amount { get; set; }
        //[ForeignKey("Employee")]
        public long? approved_by { get; set; }
    }
}
