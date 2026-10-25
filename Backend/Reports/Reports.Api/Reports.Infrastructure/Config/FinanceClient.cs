using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class FinanceClient
    {
        HttpClient client;

        public FinanceClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<FinancialReportDto>> GetFinancial(
            ReportFilterDto filter)
        {
            var data = await client.GetFromJsonAsync<List<FinancialReportDto>>(
                "api/finance/report");

            return data ?? new List<FinancialReportDto>();
        }
    }
}

