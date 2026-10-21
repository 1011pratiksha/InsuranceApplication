using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class SurveyAssignment
    {
        [Key]
        public int SurveyId { get; set; }

        public int ClaimId { get; set; }

        public int SurveyorId { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        public DateTime? InspectionDate { get; set; }

        [MaxLength(500)]
        public string? ReportPath { get; set; }

        [Required]
        [MaxLength(50)]
        public string SurveyStatus { get; set; } = "Assigned";

        public Claim? Claim { get; set; }
        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}

