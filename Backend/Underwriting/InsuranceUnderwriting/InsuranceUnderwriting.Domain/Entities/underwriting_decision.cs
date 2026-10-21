using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class underwriting_decision
    {
        [Key]
        public long decision_id { get; set; }

        [ForeignKey("underwriting")]
        public long underwriting_id { get; set; }
        public underwriting_case underwriting { get; set; } 


        public string decision { get; set; } 

        public decimal loading_percentage { get; set; }

        public string? exclusion_details { get; set; }

        public string? rejection_reason { get; set; }


        [ForeignKey("Employee")]
        public long approved_by { get; set; }
        public Employee Employee { get; set; } 

        public DateTime decision_date { get; set; }
    }
}
