using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class UnderwritingClient
    {
        HttpClient client;

        public UnderwritingClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<UnderwritingReportDto>> GetUnderwriting(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<UnderwritingReportDto>>(
                "api/underwriting/report");

            return data ?? new List<UnderwritingReportDto>();
        }
    }
}

