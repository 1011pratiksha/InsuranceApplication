using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimHistory
    {
        [Key]
        public int history_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string action { get; set; } = string.Empty;

        [MaxLength(100)]
        public string performed_by { get; set; } = string.Empty;

        public DateTime action_date { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? remarks { get; set; }

        public ClaimMaster? ClaimMaster { get; set; }
    }
}