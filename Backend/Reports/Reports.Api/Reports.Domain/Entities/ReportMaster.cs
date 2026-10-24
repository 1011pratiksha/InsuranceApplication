using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.Entities
{
    public class ReportMaster
    {
        public long ReportId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<ReportSchedule> Schedules { get; set; }
            = new List<ReportSchedule>();

        public ICollection<ReportExecution> Executions { get; set; }
            = new List<ReportExecution>();
    }
}
