using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class ServiceRequestHistory
    {
        [Key]
        public long history_id { get; set; }

        [ForeignKey("ServiceRequest")]
        public long request_id { get; set; }
        public ServiceRequest? ServiceRequest { get; set; }
        public string action { get; set; } 
        public long action_by { get; set; }
        public DateTime action_date { get; set; }
        public string? remarks { get; set; }
    }
}
