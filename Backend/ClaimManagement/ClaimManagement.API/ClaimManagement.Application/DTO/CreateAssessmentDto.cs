using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class CreateAssessmentDto
    {
        [Range(typeof(decimal), "0.01", "999999999999999")]
        public decimal AssessedAmount { get; set; }

        [Range(typeof(decimal), "0", "999999999999999")]
        public decimal Deductible { get; set; }

        [Range(typeof(decimal), "0", "999999999999999")]
        public decimal Depreciation { get; set; }

        [Range(typeof(decimal), "0", "999999999999999")]
        public decimal NonPayable { get; set; }

        [Range(typeof(decimal), "0.01", "999999999999999")]
        public decimal NetPayable { get; set; }
    }
}
