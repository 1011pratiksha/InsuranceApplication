using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ClaimManagement.Domain.Entities
{
    public class SurveyAssignment
    {
        [Key]
        public int survey_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        public int surveyor_id { get; set; }

        public DateTime assigned_date { get; set; } = DateTime.UtcNow;

        public DateTime? inspection_date { get; set; }

        [MaxLength(500)]
        public string? report_path { get; set; }

        [Required]
        [MaxLength(50)]
        public string survey_status { get; set; } = "Assigned";

        public ClaimMaster? ClaimMaster { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}