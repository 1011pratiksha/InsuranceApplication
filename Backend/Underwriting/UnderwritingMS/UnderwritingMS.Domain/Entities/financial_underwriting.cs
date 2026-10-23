
using System.ComponentModel.DataAnnotations;

namespace UnderwritingMS.Domain.Entities
{
    public class financial_underwriting
    {
        [Key]
        public int financial_id { get; set; }

        public int underwriting_id { get; set; }

        public decimal annual_income { get; set; }

        public decimal monthly_income { get; set; }

        public string employer_name { get; set; } = string.Empty;

        public string occupation { get; set; } = string.Empty;

        public decimal existing_insurance { get; set; }

        public decimal loan_liability { get; set; }
    }
}
