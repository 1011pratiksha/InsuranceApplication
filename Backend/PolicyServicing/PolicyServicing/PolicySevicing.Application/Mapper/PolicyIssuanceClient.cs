using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Mapper
{
    public class PolicyIssuanceClient
    {
        HttpClient client;
        public PolicyIssuanceClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<PolicyDTO?> FindPolicyById(long id)
        {
            return await client.GetFromJsonAsync<PolicyDTO>($"/api/v1/policies/{id}");
        }
    }
}
