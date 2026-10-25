
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Domain.Entities
{
    public class CustomerKyc
    {
        [Key]
        public int KycId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string KycNumber { get; set; } = string.Empty;

        [Required]
        public DateTime KycDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string VerificationStatus { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }
    }
}
