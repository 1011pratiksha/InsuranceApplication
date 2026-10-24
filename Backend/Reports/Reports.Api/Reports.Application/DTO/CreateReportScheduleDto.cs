using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
  public class CreateReportScheduleDto
    {
        public long ReportId { get; set; }
        public string ScheduleType { get; set; } = "Daily";
        public DateTime NextRunDate { get; set; }
        public string? EmailTo { get; set; }
    }
}
