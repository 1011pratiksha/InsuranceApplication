using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class ServiceRequest
    {
        [Key]
        public long request_id { get; set; }

        [Required]
        public string request_number { get; set; } = string.Empty;

        [ForeignKey("policy_id")]
        public long policy_id { get; set; }
        public virtual Policy? Policy { get; set; }

        [Required]
        [ForeignKey("customer_id")]
        public int customer_id { get; set; }
        public  Customer? Customer { get; set; }

        [Required]
        public string request_type { get; set; }

        [Required]
        public DateTime request_date { get; set; }

        [Required]
        public string status { get; set; } 

        public List<PolicyEndorsement> Endorsements { get; set; }

        public List<ServiceRequestHistory> History { get; set; }
            
    }
}
