
using InsuranceAgent.Application.DTO;
using System.Net.Http.Json;

namespace InsuranceAgent.Application.Mapper
{
    public class UserClient
    {
        private readonly HttpClient client;

        public UserClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            return await client.GetFromJsonAsync<UserDTO>(
                $"api/v1/users/{id}");
        }
    }
}
