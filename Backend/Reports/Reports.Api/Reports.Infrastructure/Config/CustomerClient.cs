using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class CustomerClient
    {
        private readonly HttpClient client;

        public CustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<CustomerReportDto>> GetCustomers(
            ReportFilterDto filter)
        {
            var response =
                await client.GetFromJsonAsync<List<CustomerReportDto>>(
                    "api/customers/report");

            return response ?? new List<CustomerReportDto>();
        }
    }
}

