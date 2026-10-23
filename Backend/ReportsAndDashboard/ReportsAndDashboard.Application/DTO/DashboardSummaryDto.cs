
namespace ReportsAndDashboard.Application.DTO
{
  public class DashboardSummaryDto
  {
    public int TotalCustomers { get; set; }
    public int TotalPolicies { get; set; }
    public int ActivePolicies { get; set; }
    public decimal PremiumCollected { get; set; }
    public decimal PendingPremium { get; set; }
    public int ClaimsRegistered { get; set; }
    public int ClaimsApproved { get; set; }
    public int ClaimsRejected { get; set; }
    public int ClaimsSettled { get; set; }
    public int RenewalDue { get; set; }
    public decimal Revenue { get; set; }
  }
}
