using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class UnderwritingReportDto
    {
        public long CaseId { get; set; }
        public long CustomerId { get; set; }
        public string? Status { get; set; }
        public string? RiskLevel { get; set; }
        public bool MedicalPending { get; set; }
        public long? UnderwriterId { get; set; }
        public DateTime? DecisionDate { get; set; }
    }
}
