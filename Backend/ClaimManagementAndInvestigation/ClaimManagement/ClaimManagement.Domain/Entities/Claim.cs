using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClaimNumber { get; set; } = string.Empty;

        public int PolicyId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClaimType { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

        public decimal EstimatedLoss { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClaimStatus { get; set; } = "Registered";

        public ICollection<ClaimDocument> Documents { get; set; } = new List<ClaimDocument>();

        public ICollection<ClaimAssessment> Assessments { get; set; } = new List<ClaimAssessment>();

        public ICollection<ClaimHistory> History { get; set; } = new List<ClaimHistory>();

        public ICollection<SurveyAssignment> SurveyAssignments { get; set; } = new List<SurveyAssignment>();
    }
}

