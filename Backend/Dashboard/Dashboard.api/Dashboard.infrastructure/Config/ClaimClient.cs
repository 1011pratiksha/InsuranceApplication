using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class ClaimClient
    {
        private readonly HttpClient client;

        public ClaimClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<ClaimResponseDto>> GetAllClaimsAsync()
        {
            var claims = await client.GetFromJsonAsync<List<ClaimResponseDto>>("api/Claims");

            return claims ?? new List<ClaimResponseDto>();
        }
    }

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