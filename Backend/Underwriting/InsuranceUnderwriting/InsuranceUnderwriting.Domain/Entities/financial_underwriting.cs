using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class financial_underwriting
    {
        [Key]
        public long financial_id { get; set; }


        [ForeignKey("underwriting")]
        public long underwriting_id { get; set; }
        public underwriting_case underwriting { get; set; } 


        public decimal annual_income { get; set; }

        public string? employer_name { get; set; }

        public string? occupation { get; set; }

        public decimal existing_insurance { get; set; }

        public decimal loan_liability { get; set; }
    }
}
