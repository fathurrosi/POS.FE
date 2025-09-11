using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using System.Text;

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

        public async Task<PagingResult<Usp_GetRolePagingResult>> GetPagingAsync(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Role/Paging/{pageIndex}/{pageSize}");
            response.EnsureSuccessStatusCode();
            PagingResult<Usp_GetRolePagingResult> result = await response.Content.ReadFromJsonAsync<PagingResult<Usp_GetRolePagingResult>>();

            return result;
        }

        public async Task<Role> GetDataByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Role/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Role>();
        }

        public async Task<int> Save(Role item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Role", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return int.Parse(result);
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Role/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return int.Parse(result);
            }
            else
            {
                return -1;
            }
        }

    }
}
