using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POS.Domain.Models;
using POS.Domain.Models.Response;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace POS.Presentation.Handlers
{
    public class JwtAuthHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IDistributedCache _cache;

        public JwtAuthHeaderHandler(IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            //_httpContextAccessor = httpContextAccessor;
            //_cache = cache;
            _httpContextAccessor = httpContextAccessor;
            //_authService = authService;
        }

        //public async Task<RefreshTokenResponse> GetTokenAsync()
        //{
        //    var tokenJson = await _cache.GetStringAsync("POSToken");
        //    if (tokenJson != null)
        //    {
        //        return JsonConvert.DeserializeObject<RefreshTokenResponse>(tokenJson);
        //    }
        //    return null;
        //}


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //var token = httpContext?.Request.Cookies["token"];

            //RefreshTokenResponse rtResponse = await GetTokenAsync();
            UserData? data = Utilities.GetLoginInfo(httpContext);
            if (data != null && !string.IsNullOrEmpty(data.Token.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", data.Token.AccessToken);
            }

            return await base.SendAsync(request, cancellationToken);

            //var httpContext = _httpContextAccessor.HttpContext;
            //RefreshTokenResponse rtResponse = await GetLoginInfo();
            //if (rtResponse != null && !string.IsNullOrEmpty(rtResponse.AccessToken))
            //{
            //    if (rtResponse.RefreshTokenExpires < DateTime.UtcNow)
            //    {
            //        RefreshTokenResponse newAccessToken = await _authService.RefreshTokenAsync(new Domain.Models.Request.RefreshTokenRequest { RefreshToken = rtResponse.RefreshToken });
            //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken.AccessToken);
            //    }
            //    else
            //    {
            //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", rtResponse.AccessToken);
            //    }
            //}

            //return await base.SendAsync(request, cancellationToken);

        }
    }
}
