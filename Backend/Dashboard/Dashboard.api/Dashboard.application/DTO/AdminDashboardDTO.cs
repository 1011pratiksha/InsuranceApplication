namespace Dashboard.application.DTO
{
    public class AdminDashboardDTO
    {
        public int TotalCustomers { get; set; }
        public int TotalPolicies { get; set; }
        public int ActivePolicies { get; set; }
        public decimal PremiumCollected { get; set; }
        public decimal PendingPremium { get; set; }
        public int ClaimsRegistered { get; set; }
        public int ClaimsSettled { get; set; }
        public decimal OutstandingClaims { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public string TopPerformingAgent { get; set; } = string.Empty;
    }
}