
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class CustomerReportFilterDto
  {
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public long? CityId { get; set; }

    public long? StateId { get; set; }

    public long? ProductId { get; set; }

    public long? AgentId { get; set; }

    public string? CustomerStatus { get; set; }
  }
}
