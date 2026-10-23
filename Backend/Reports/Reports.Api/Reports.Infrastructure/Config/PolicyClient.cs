using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class PolicyClient
    {
        HttpClient client;

        public PolicyClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<PolicyReportDto>> GetPolicies(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<PolicyReportDto>>(
                "api/policies/report");

            return data ?? new List<PolicyReportDto>();
        }
    }
}

