
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Application.DTO
{
    // Fields that can be changed in an existing KYC request.
    public class UpdateCustomerKycDto
    {
        // The customer associated with the KYC request.
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        // Optional remarks about the KYC request.
        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}
