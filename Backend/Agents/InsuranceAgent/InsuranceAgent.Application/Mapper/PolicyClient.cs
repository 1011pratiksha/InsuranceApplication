using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

    }
}
