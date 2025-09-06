using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Presentation.Services.Interfaces;
using System.Text;

namespace POS.Presentation.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;
        public MenuService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<List<Menu>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/Menu");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Menu>>();

        }

        public async Task<Menu> GetDataByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Menu/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Menu>();
        }

        public async Task<List<Menu>> GetDataByUsernameAsync(string username)
        {
            var response = await _httpClient.GetAsync($"api/Menu/{username}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Menu>>();
        }

        public async Task<PagingResult<Usp_GetMenuPagingResult>> GetPagingAsync(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Menu/Paging/{pageIndex}/{pageSize}");
            response.EnsureSuccessStatusCode();
            PagingResult<Usp_GetMenuPagingResult> result = await response.Content.ReadFromJsonAsync<PagingResult<Usp_GetMenuPagingResult>>();

            return result;
        }


        public async Task<int> Save(Menu item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Menu", content);
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
            var response = await _httpClient.DeleteAsync($"api/Menu/{id}");
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
