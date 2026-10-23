using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class UploadDocumentDto
    {
        [Required]
        public string DocumentType { get; set; } = string.Empty; // Aadhaar, PAN, Passport, Driving License, Photo, Income Proof, Address Proof

        public string? DocumentNumber { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;
    }

    public class CustomerDocumentDto : UploadDocumentDto
    {
        public int DocumentId { get; set; }
        public int CustomerId { get; set; }
        public DateTime UploadDate { get; set; }
        public string VerificationStatus { get; set; } = "Pending";
    }
}
