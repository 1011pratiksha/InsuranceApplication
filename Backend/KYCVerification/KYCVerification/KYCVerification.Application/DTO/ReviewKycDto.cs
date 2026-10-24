
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Application.DTO
{
    // Remarks supplied when reviewing KYC or a document.
    public class ReviewKycDto
    {
        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}
