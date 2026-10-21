using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Domain.Entities.Reporting
{
  public enum ReportScheduleType
  {
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    Yearly
  }

  public enum ReportRecordStatus
  {
    Active,
    Inactive
  }

  public enum ReportExecutionStatus
  {
    Success,
    Failed
  }
}
