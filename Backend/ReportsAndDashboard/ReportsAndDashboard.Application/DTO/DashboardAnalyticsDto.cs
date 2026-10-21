
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class DashboardAnalyticsDto
  {
    public long SnapshotId { get; set; }

    public DateTime SnapshotDate { get; set; }

    public int TotalCustomers { get; set; }

    public int TotalPolicies { get; set; }

    public decimal PremiumCollected { get; set; }

    public decimal ClaimsPaid { get; set; }

    public decimal Revenue { get; set; }
  }
}
