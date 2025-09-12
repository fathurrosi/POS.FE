using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models.Request;
using POS.Domain.Models.Response;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using System.Security.Claims;
using System.Text;

namespace POS.Presentation.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest resquest)
        {
            var json = JsonConvert.SerializeObject(resquest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Auth/Refresh", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RefreshTokenResponse>();
            }

            return null;
        }

        public async Task<LoginResponse<User>> LoginAsync(LoginRequest item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Auth/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponse<User>>();
                }
                else
                {
                    return new LoginResponse<User>
                    {
                        Success = false,
                        Message = "Failed to retrieve token",
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse<User>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

    }
}
