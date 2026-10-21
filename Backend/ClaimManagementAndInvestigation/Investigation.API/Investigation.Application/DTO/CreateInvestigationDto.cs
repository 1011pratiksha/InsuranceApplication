using System.ComponentModel.DataAnnotations;

namespace Investigation.Application.DTO
{
    public class CreateInvestigationDto
    {
        public int? InvestigatorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string InvestigationReason { get; set; } = string.Empty;
    }
}