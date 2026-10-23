using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimDocument
    {
        [Key]
        public int DocumentId { get; set; }

        public int ClaimId { get; set; }

        [Required]
        [MaxLength(150)]
        public string DocumentName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? FilePath { get; set; }

        [Required]
        [MaxLength(50)]
        public string VerificationStatus { get; set; } = "Pending";

        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public Claim? Claim { get; set; }
    }
}

