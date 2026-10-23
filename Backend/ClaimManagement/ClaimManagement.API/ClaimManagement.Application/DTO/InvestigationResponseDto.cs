namespace ClaimManagement.Application.DTO
{
    public class InvestigationResponseDto
    {
        public int InvestigationId { get; set; }
        public int ClaimId { get; set; }
        public int? InvestigatorId { get; set; }
        public string InvestigationReason { get; set; } = string.Empty;
        public string InvestigationStatus { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? ReportPath { get; set; }
    }
}