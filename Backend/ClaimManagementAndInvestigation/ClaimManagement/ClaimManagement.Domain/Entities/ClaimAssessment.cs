using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimAssessment
    {
        [Key]
        public int AssessmentId { get; set; }

        public int ClaimId { get; set; }

        public decimal AssessedAmount { get; set; }

        public decimal Deductible { get; set; }

        public decimal Depreciation { get; set; }

        public decimal NonPayable { get; set; }

        public decimal NetPayable { get; set; }

        public DateTime AssessmentDate { get; set; } = DateTime.UtcNow;

        public Claim? Claim { get; set; }
    }
}

