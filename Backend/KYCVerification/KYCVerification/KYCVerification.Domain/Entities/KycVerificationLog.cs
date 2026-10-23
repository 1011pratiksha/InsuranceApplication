
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Domain.Entities
{
    public class KycVerificationLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int KycId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        public int? PerformedBy { get; set; }

        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}
