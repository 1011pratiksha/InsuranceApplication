using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class UpdateReportScheduleDto
    {
        public string ScheduleType { get; set; } = "Daily";
        public DateTime NextRunDate { get; set; }
        public string? EmailTo { get; set; }
        public string Status { get; set; } = "Active";
    }
}
