using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class risk_score
    {

        [Key]
        public long risk_id { get; set; }


        [ForeignKey("underwriting")]
        public long underwriting_id { get; set; }
        public underwriting_case underwriting { get; set; } 


        public int medical_score { get; set; }

        public int occupation_score { get; set; }

        public int lifestyle_score { get; set; }

        public int financial_score { get; set; }

        public int total_score { get; set; }

        public string risk_category { get; set; } 

    }
}
