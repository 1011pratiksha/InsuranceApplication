using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.DTO
{
    public class FinancialDetailsResponseDto
    {
        public long financial_id { get; set; }
        public long underwriting_id { get; set; }
        public decimal annual_income { get; set; }
        public string? employer_name { get; set; }
        public string? occupation { get; set; }
        public decimal existing_insurance { get; set; }
        public decimal loan_liability { get; set; }
    }
}
