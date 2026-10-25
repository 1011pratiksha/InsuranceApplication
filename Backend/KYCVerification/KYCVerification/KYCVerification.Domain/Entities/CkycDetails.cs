
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Domain.Entities
{
    public class CkycDetails
    {
        [Key]
        public int CkycId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(30)]
        public string? CkycNumber { get; set; }

        public DateTime? VerificationDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string CkycStatus { get; set; } = "Pending";
    }
}
