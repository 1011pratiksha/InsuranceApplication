
namespace KycVerification.Application.DTO
{
    // Records actions performed during KYC verification.
    public class KycVerificationLogDto
    {
        public int LogId { get; set; }
        public int KycId { get; set; }
        public string Action { get; set; } = string.Empty;
        public int? PerformedBy { get; set; }
        public DateTime ActionDate { get; set; }
        public string? Remarks { get; set; }
    }
}
