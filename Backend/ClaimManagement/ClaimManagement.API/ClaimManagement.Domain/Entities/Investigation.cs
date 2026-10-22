using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimManagement.Domain.Entities
{
    public class Investigation
    {
        [Key]
        public int investigation_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        public ClaimMaster? ClaimMaster { get; set; }

        public int? investigator_id { get; set; }

        [Required]
        [MaxLength(500)]
        public string investigation_reason { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? report_path { get; set; }

        [Required]
        [MaxLength(50)]
        public string investigation_status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? AssignedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}