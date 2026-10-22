using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class UpdateClaimDto
    {
        [Required]
        [MaxLength(50)]
        public string ClaimType { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        [Range(typeof(decimal), "0.01", "999999999999999")]
        public decimal EstimatedLoss { get; set; }
    }
}