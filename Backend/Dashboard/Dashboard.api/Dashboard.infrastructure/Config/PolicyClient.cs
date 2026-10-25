using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class PolicyClient
    {
        private readonly HttpClient client;

    public PolicyClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<PolicyDto>> GetAllPoliciesAsync()
        {
            var result = await client.GetFromJsonAsync<List<PolicyDto>>("api/v1/policies");

            return result ?? new List<PolicyDto>();
        }
    }

    public class PolicyDto
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public int QuoteId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PolicyStatus { get; set; } = string.Empty;
    }

}
