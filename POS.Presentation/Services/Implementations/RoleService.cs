using POS.Domain.Entities;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        public RoleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }


        public async Task<List<Role>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/Role");
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
            var data = await response.Content.ReadFromJsonAsync<List<Role>>();
            return data;
        }

        // POST example
        public async Task<Role> CreateDataAsync(RoleModel newData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Role", newData);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<Role>();
            return createdData;
        }

        public async Task<List<Role>> GetByUsername(string username)
        {
            var response = await _httpClient.GetAsync($"api/Role/{username}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Role>>();
        }

    }
}
