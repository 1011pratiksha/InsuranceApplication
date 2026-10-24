
using InsuranceAgent.Application.DTO;
using System.Net.Http.Json;

namespace InsuranceAgent.Application.Mapper
{
    public class ProductClient
    {
        private readonly HttpClient client;

        public ProductClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ProductDTO?> GetProductById(int id)
        {
            return await client.GetFromJsonAsync<ProductDTO>(
                $"api/v1/products/{id}");
        }
    }
}
