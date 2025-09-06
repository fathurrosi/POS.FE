using POS.Domain.Entities;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Services.Implementations
{
    public class PrevillageService : IPrevillageService
    {
        private readonly HttpClient _httpClient;
        public PrevillageService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<Previllage>> GetByRole(string role)
        {
            var response = await _httpClient.GetAsync($"api/Previllage/{role}");
            response.EnsureSuccessStatusCode();
            var userPrevillages = await response.Content.ReadFromJsonAsync<List<Previllage>>();
            return userPrevillages ?? new List<Previllage>();
        }

        public async Task<List<VUserPrevillage>> GetByUsername(string username)
        {
            var response = await _httpClient.GetAsync($"api/Previllage/{username}");
            response.EnsureSuccessStatusCode();
            var userPrevillages = await response.Content.ReadFromJsonAsync<List<VUserPrevillage>>();
            return userPrevillages ?? new List<VUserPrevillage>();
        }
    }
}
