
namespace KycVerification.Application.DTO
{
    // Holds the summary counts displayed on the KYC dashboard.
    public class KycDashboardDto
    {
        public int TotalRequests { get; set; }

        public int PendingRequests { get; set; }

        public int VerifiedRequests { get; set; }

        public int RejectedRequests { get; set; }

        public int CkycSuccessful { get; set; }

        public int CkycFailed { get; set; }

        // Number of KYC requests created today.
        public int RequestsCreatedToday { get; set; }
    }
}
