using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int ClaimId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [MaxLength(100)]
        public string PerformedBy { get; set; } = string.Empty;

        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public Claim? Claim { get; set; }
    }
}

