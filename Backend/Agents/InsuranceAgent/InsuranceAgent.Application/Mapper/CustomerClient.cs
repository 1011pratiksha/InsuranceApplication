
using InsuranceAgent.Application.DTO;
using System.Net.Http.Json;

namespace InsuranceAgent.Application.Mapper
{
    public class CustomerClient
    {
        private readonly HttpClient client;

        public CustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CustomerDTO?> GetCustomerById(int id)
        {
            return await client.GetFromJsonAsync<CustomerDTO>(
                $"api/v1/customers/{id}");
        }
    }
}
