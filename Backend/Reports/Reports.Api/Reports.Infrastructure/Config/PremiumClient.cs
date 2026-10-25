using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class PremiumClient
    {
        HttpClient client;

        public PremiumClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<PremiumReportDto>> GetPremiums(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<PremiumReportDto>>(
                "api/premiums/report");

            return data ?? new List<PremiumReportDto>();
        }
    }

}
