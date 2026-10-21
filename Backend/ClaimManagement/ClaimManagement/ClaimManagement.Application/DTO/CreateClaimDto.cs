using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class CreateClaimDto
    {
        [Range(1, long.MaxValue)]
        public long PolicyId { get; set; }

        [Range(1, long.MaxValue)]
        public long CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClaimType { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        [Range(typeof(decimal), "0.01", "999999999999999")]
        public decimal EstimatedLoss { get; set; }
    }
}