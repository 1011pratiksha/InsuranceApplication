using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.Entities
{
   public class ReportSchedule
    {
        public long ScheduleId { get; set; }
        public long ReportId { get; set; }

        public string ScheduleType { get; set; } = "Daily";
        public DateTime NextRunDate { get; set; }
        public string? EmailTo { get; set; }
        public string Status { get; set; } = "Active";

        public ReportMaster? Report { get; set; }
    }
}
