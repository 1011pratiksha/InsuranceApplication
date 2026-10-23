using System;

namespace CustomerRegistration.Application.DTO
{
    public class DocumentResponseDto
    {
        public int DocumentId { get; set; }
        public int CustomerId { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public string VerificationStatus { get; set; } = "Pending";
    }
}
