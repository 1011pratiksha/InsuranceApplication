namespace ClaimManagement.Application.DTO
{
    public class SurveyResponseDto
    {
        public int SurveyId { get; set; }
        public int ClaimId { get; set; }
        public int SurveyorId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string? ReportPath { get; set; }
        public string? Remarks { get; set; }
        public string SurveyStatus { get; set; } = string.Empty;
    }
}