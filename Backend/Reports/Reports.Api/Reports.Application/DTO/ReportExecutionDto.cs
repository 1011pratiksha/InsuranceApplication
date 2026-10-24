using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class ReportExecutionDto
    {
        public long ExecutionId { get; set; }
        public long ReportId { get; set; }
        public long ExecutedBy { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string ExecutionStatus { get; set; } = string.Empty;
        public string? FilePath { get; set; }
    }
}
