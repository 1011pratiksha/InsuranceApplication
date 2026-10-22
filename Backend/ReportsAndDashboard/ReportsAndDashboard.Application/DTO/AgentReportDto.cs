
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class AgentReportDto
  {
    public long AgentId { get; set; }

    public string AgentCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime JoiningDate { get; set; }

    public string Status { get; set; } = string.Empty;
  }
}
