using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class underwriting_audit_log
    {
        [Key]
        public long audit_id { get; set; }


        [ForeignKey("underwriting")]
        public long underwriting_id { get; set; }
        public underwriting_case underwriting { get; set; } 


        public string action { get; set; }


        [ForeignKey("Employee")]
        public long performed_by { get; set; }
        public Employee Employee { get; set; }


        public string? remarks { get; set; }

        public DateTime action_date { get; set; }
    }
}
