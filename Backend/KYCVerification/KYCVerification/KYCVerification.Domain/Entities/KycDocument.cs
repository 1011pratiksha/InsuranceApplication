
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Domain.Entities
{
    public class KycDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int KycId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(20)]
        public string VerificationStatus { get; set; } = "Pending";
    }
}
