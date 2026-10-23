namespace Dashboard.application.DTO
{
    public class CustomerDashboardDTO
    {
        public int ActivePolicies { get; set; }

        public DateTime? NextPremiumDue { get; set; }

        public int OpenClaims { get; set; }

        public string KYCStatus { get; set; } = string.Empty;
    }

}
