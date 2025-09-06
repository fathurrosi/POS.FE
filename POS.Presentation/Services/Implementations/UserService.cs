using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POS.Presentation.Services.Implementations
{
    //public class UserService : SignInManager<IdentityUser>, IUserService
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<List<UserModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("api/User");
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code
            var data = await response.Content.ReadFromJsonAsync<List<UserModel>>();
            return data;
        }

        public async Task<UserModel> GetByUsername(UserModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<UserModel>();
            return createdData;
        }

        public async Task<UserModel> GetByUsername(string username, string password, bool rememberMe)
        {
            UserModel model = new UserModel();
            model.Username = username;
            model.Password = password;
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<UserModel>();
            return createdData;
        }


        public async Task<UserModel> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"api/User/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }

    }
}
