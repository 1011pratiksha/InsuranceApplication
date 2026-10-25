using System.ComponentModel.DataAnnotations;

namespace Investigation.Domain.Entities
{
    public class InvestigationCase
    {
        [Key]
        public int InvestigationId { get; set; }

        public int ClaimId { get; set; }

        public int? InvestigatorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string InvestigationReason { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ReportPath { get; set; }

        [Required]
        [MaxLength(50)]
        public string InvestigationStatus { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? AssignedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public ICollection<InvestigationHistory> History { get; set; } = new List<InvestigationHistory>();
    }
}

