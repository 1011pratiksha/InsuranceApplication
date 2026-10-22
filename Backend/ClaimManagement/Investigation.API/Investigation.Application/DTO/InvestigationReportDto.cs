using System.ComponentModel.DataAnnotations;

namespace Investigation.Application.DTO
{
    public class InvestigationReportDto
    {
        [Required]
        [MaxLength(500)]
        public string ReportPath { get; set; } = string.Empty;
    }
}