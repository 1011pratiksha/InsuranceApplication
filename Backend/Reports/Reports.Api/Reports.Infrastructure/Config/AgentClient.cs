using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class AgentClient
    {
        HttpClient client;

        public AgentClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<AgentReportDto>> GetAgents(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<AgentReportDto>>(
                "api/agents/report");

            return data ?? new List<AgentReportDto>();
        }
    }
}

