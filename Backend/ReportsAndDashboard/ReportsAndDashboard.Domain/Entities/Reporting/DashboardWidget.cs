
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities.Reporting
{
  public class DashboardWidget
  {
    [Key]
    public long WidgetId { get; set; }

    public string WidgetName { get; set; } = string.Empty;

    public string WidgetType { get; set; } = string.Empty;

    public int SequenceNo { get; set; }

    public ReportRecordStatus Status { get; set; }
  }
}
