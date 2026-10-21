namespace ClaimManagement.Application.DTO
{
    public class ClaimResponseDto
    {
        public long ClaimId { get; set; }

        public string ClaimNumber { get; set; } = string.Empty;

        public long PolicyId { get; set; }

        public long CustomerId { get; set; }

        public string ClaimType { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        public DateTime ClaimDate { get; set; }

        public decimal EstimatedLoss { get; set; }

        public string ClaimStatus { get; set; } = string.Empty;
    }
}