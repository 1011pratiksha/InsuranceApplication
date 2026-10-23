using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.Entities
{
    public class ReportExecution
    {
        public long ExecutionId { get; set; }
        public long ReportId { get; set; }
        public long ExecutedBy { get; set; }

        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
        public string ExecutionStatus { get; set; } = "Success";
        public string? FilePath { get; set; }

        public ReportMaster? Report { get; set; }
    }
}
