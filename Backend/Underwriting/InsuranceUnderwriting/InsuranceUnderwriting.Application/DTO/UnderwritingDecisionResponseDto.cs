using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.DTO
{
    public class UnderwritingDecisionResponseDto
    {
        public long decision_id { get; set; }

        public long underwriting_id { get; set; }

        public string decision { get; set; }

        public decimal loading_percentage { get; set; }

        public string? exclusion_details { get; set; }

        public string? rejection_reason { get; set; }

        public long approved_by { get; set; }

        public DateTime decision_date { get; set; }

    }
}
