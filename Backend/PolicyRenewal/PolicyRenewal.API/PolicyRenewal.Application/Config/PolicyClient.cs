using PolicyRenewal.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.Config
{
    public class PolicyClient
    {
        HttpClient client;
        public PolicyClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PolicyDto> GetPolicyById(int policyId)
        {
            return await client.GetFromJsonAsync<PolicyDto>($"/policies/{policyId}");
        }
    }
}
