
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClaimSettlement.Infrastructure.Integrations
{
    public class ClaimManagementClient
    {
        private readonly HttpClient client;

        public ClaimManagementClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<JsonElement?> FindClaimById(long claimId)
        {
            var response = await client.GetAsync($"api/Claims/{claimId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }
    }
}
