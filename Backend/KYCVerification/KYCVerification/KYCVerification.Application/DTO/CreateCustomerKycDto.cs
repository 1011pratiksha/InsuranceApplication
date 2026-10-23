
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Application.DTO
{
    // Data required to start a KYC request.
    public class CreateCustomerKycDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
    }
}
