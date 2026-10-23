
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities.Reporting
{
  public class ReportSchedule
  {
    [Key]
    public long ScheduleId { get; set; }

    public long ReportId { get; set; }

    public ReportScheduleType ScheduleType { get; set; }

    public DateTime NextRunDate { get; set; }

    public string? EmailTo { get; set; }

    public ReportRecordStatus Status { get; set; }

    // Foreign key relationship

    [ForeignKey(nameof(ReportId))]
    public ReportMaster Report { get; set; } = null!;
  }
}
