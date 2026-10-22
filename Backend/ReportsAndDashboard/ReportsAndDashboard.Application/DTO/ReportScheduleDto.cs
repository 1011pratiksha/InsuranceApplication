
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class ReportScheduleDto
  {
    public long ScheduleId { get; set; }
    public long ReportId { get; set; }
    public string ScheduleType { get; set; } = string.Empty;
    public DateTime? NextRunDate { get; set; }
    public string EmailTo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
  }
}
