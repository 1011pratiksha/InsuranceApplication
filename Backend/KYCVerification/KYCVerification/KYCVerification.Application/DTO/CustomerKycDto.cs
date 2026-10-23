namespace KycVerification.Application.DTO
{
    // KYC details returned to the API caller.
    public class CustomerKycDto
    {
        public int KycId { get; set; }
        public int CustomerId { get; set; }
        public string? KycNumber { get; set; }
        public DateTime KycDate { get; set; }
        public string? VerificationStatus { get; set; }
        public string? Remarks { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerifiedDate { get; set; }
    }
}