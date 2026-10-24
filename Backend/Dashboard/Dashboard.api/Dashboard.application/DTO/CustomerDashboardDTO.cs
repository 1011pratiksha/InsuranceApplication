namespace Dashboard.application.DTO
{
    public class CustomerDashboardDTO
    {
        public int TotalPolicies { get; set; }
        public int ActivePolicies { get; set; }
        public decimal UpcomingPremium { get; set; }
        public decimal TotalPremiumPaid { get; set; }
        public int TotalClaims { get; set; }
        public int PendingClaims { get; set; }
        public int SettledClaims { get; set; }
        public int UnreadNotifications { get; set; }
    }
}