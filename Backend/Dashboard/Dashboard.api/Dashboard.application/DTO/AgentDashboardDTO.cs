namespace Dashboard.application.DTO
{
    public class AgentDashboardDTO
    {
        public int TotalCustomers { get; set; }
        public int TotalPolicies { get; set; }
        public int ActivePolicies { get; set; }
        public decimal PremiumCollected { get; set; }
        public decimal PendingPremium { get; set; }
        public int ClaimsRegistered { get; set; }
        public decimal TotalCommission { get; set; }
    }
}