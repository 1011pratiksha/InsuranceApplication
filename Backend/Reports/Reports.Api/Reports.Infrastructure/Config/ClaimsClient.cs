using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class ClaimsClient
    {
        HttpClient client;

        public ClaimsClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<ClaimReportDto>> GetClaims(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<ClaimReportDto>>(
                "api/claims/report");

            return data ?? new List<ClaimReportDto>();
        }
    }
}

