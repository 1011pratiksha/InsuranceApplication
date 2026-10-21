using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class medical_underwriting
    {
        [Key]
        public long medical_id { get; set; }


        [ForeignKey("underwriting")]
        public long underwriting_id { get; set; }
        public underwriting_case underwriting { get; set; }


        public decimal height { get; set; }

        public decimal weight { get; set; }

        public decimal bmi { get; set; }

        public bool smoking { get; set; }

        public bool alcohol { get; set; }

        public string? medical_history { get; set; }

        public string? medical_report_path { get; set; }
    }
}
