namespace ClaimManagement.Application.DTO
{
    public class ClaimDocumentResponseDto
    {
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string VerificationStatus { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
    }
}