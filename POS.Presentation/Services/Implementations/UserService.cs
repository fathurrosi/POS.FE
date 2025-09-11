using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models.Result;
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



        public async Task<PagingResult<Usp_GetUserPagingResult>> GetPagingAsync(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/User/Paging/{pageIndex}/{pageSize}");
            response.EnsureSuccessStatusCode();
            PagingResult<Usp_GetUserPagingResult> result = await response.Content.ReadFromJsonAsync<PagingResult<Usp_GetUserPagingResult>>();

            return result;
        }



        public async Task<UserModel> GetByUsername(UserModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<UserModel>();
            return createdData;
        }

        public async Task<User> GetByUsername(string username, string password, bool rememberMe)
        {
            User model = new User();
            model.Username = username;
            model.Password = password;
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            var createdData = await response.Content.ReadFromJsonAsync<User>();
            return createdData;
        }


        public async Task<UserModel> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"api/User/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }

        Task<List<User>> IUserService.GetDataAsync()
        {
            throw new NotImplementedException();
        }

        Task<User> IUserService.GetByUsername(UserModel item)
        {
            throw new NotImplementedException();
        }

        Task<User> IUserService.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
