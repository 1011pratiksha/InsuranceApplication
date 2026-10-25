namespace KycVerification.Application.DTO
{
    // Central KYC Registry information.
    public class CkycDetailsDto
    {
        public int CkycId { get; set; }
        public int CustomerId { get; set; }
        public string? CkycNumber { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string? CkycStatus { get; set; }
    }
}
