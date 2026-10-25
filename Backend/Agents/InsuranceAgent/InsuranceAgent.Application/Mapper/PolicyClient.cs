using InsuranceAgent.Application.DTO;
using System.Net.Http.Json;

namespace InsuranceAgent.Application.Mapper
{
    public class PolicyClient
    {
        private readonly HttpClient client;

        public PolicyClient(HttpClient client)
        {
            this.client = client;
        }


        public async Task<PolicyDTO?> GetPolicyById(int id)
        {
            return await client.GetFromJsonAsync<PolicyDTO>($"api/v1/policies/{id}");
        }

        public async Task<List<PolicyDTO>> GetAllPolicies()
        {
            return await client.GetFromJsonAsync<List<PolicyDTO>>("api/v1/policies") ?? new List<PolicyDTO>();
        }
    }
}