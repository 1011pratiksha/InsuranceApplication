using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class SurveyReportDto
    {
        public DateTime? InspectionDate { get; set; }

        [MaxLength(500)]
        public string? ReportPath { get; set; }

        [Required]
        [MaxLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}