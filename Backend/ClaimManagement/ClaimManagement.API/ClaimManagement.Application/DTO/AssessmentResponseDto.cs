namespace ClaimManagement.Application.DTO
{
    public class AssessmentResponseDto
    {
        public long AssessmentId { get; set; }
        public long ClaimId { get; set; }
        public decimal AssessedAmount { get; set; }
        public decimal Deductible { get; set; }
        public decimal Depreciation { get; set; }
        public decimal NonPayable { get; set; }
        public decimal NetPayable { get; set; }
    }
}
