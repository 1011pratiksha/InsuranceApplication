using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class PolicyEndorsement
    {
        [Key]
        public long endorsement_id { get; set; }
        [ForeignKey("ServiceRequest")]
        public long request_id { get; set; }
        public  ServiceRequest? ServiceRequest { get; set; }
        public string endorsement_number { get; set; }
        public string endorsement_type { get; set; } 
        public string? previous_value { get; set; }
        public string? updated_value { get; set; }
        public string approval_status { get; set; } 
    }
}
