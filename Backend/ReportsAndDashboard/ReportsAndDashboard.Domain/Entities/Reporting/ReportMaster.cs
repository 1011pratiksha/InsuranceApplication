
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities.Reporting
{
  public class ReportMaster
  {
    [Key]
    public long ReportId { get; set; }

    public string ReportName { get; set; } = string.Empty;

    public string ReportType { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation collections

    public List<ReportSchedule> Schedules { get; set; }
        = new List<ReportSchedule>();

    public List<ReportExecution> Executions { get; set; }
        = new List<ReportExecution>();
  }
}
