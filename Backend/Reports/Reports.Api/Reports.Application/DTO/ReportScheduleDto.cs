using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class ReportScheduleDto
    {
        public long ScheduleId { get; set; }
        public long ReportId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string ScheduleType { get; set; } = string.Empty;
        public DateTime NextRunDate { get; set; }
        public string? EmailTo { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
