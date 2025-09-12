using Azure;
using Azure.Core;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POS.Domain.Models;
using POS.Domain.Models.Request;
using POS.Domain.Models.Response;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using System.Text;

namespace POS.Presentation.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        public JwtMiddleware(RequestDelegate next, IDistributedCache cache, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task<JwtToken?> GetTokenAsync(HttpContext context)
        {
            UserData? userData = Utilities.GetLoginInfo(context);
            if (userData != null && userData.Token != null)
            {
                return userData.Token;
            }
            return null;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                UserData? userData = Utilities.GetLoginInfo(context);
                if (userData != null && IsAccessTokenExpired(userData.Token))
                {
                    RefreshTokenResponse rtResponse = await _authService.RefreshTokenAsync(new RefreshTokenRequest { RefreshToken = userData.Token.RefreshToken });
                    if (rtResponse != null)
                    {
                        userData.Token = new JwtToken { AccessToken = rtResponse.AccessToken, RefreshToken = rtResponse.RefreshToken, RefreshTokenExpires = rtResponse.RefreshTokenExpires };
                        var userDataJson = JsonConvert.SerializeObject(userData);
                        var base64data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userDataJson));
                        context.Session.SetString($"UserData_{context.User.Identity.Name}", base64data);
                    }

                }
            }

            await _next(context);
        }

        private bool IsAccessTokenExpired(JwtToken rtResponse)
        {
            if (rtResponse != null && !string.IsNullOrEmpty(rtResponse.AccessToken))
            {
                if (rtResponse.RefreshTokenExpires >= DateTime.UtcNow)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
