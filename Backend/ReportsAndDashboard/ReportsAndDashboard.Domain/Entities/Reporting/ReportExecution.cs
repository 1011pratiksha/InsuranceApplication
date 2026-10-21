
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities.Reporting
{
  public class ReportExecution
  {
    [Key]
    public long ExecutionId { get; set; }

    public long ReportId { get; set; }

    public long? ExecutedBy { get; set; }

    public DateTime ExecutionDate { get; set; }

    public ReportExecutionStatus ExecutionStatus { get; set; }

    public string? FilePath { get; set; }

    // Foreign key relationship

    [ForeignKey(nameof(ReportId))]
    public ReportMaster Report { get; set; } = null!;
  }
}
