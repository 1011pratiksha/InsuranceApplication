using System.Net.Http.Json;
using ClaimManagement.Application.DTO;

namespace ClaimManagement.Infrastructure.Config
{
    public class PolicyClient
    {
        HttpClient client;

        public PolicyClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PolicyDto?> GetPolicyById(int policyId)
        {
            return await client.GetFromJsonAsync<PolicyDto>($"/policies/{policyId}");
        }
    }
}
