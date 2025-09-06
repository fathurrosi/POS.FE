using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace POS.Presentation.Filters
{
    public class POSAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private string _screen;
        public POSAuthorizeFilter(string screen)
        {
            _screen = screen;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            string username = string.Format("{0}", user.Identity?.Name);

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            string protectedUserData = context.HttpContext.Session.GetString($"UserData_{username}");

            if (string.IsNullOrEmpty(protectedUserData))
            {
                context.Result = new ChallengeResult();
                return;
            }

            if (!HasAccess(user, protectedUserData))
            {
                context.Result = new ForbidResult();
                return;
            }

            return;
        }

        private bool HasAccess(ClaimsPrincipal user, string protectedUserData)
        {

            List<string> roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            if (!string.IsNullOrEmpty(protectedUserData))
            {
                UserData? userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
                if (userData == null) return false;

                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == _screen && roles.Contains(t.Role)).ToList();
                bool allowRead = prevItems.Where(t => t.AllowRead == true).Any();
                if (userData != null && userData.Previllages.Count() > 0 && allowRead)
                {
                    // Example: Log the username
                    Console.WriteLine($"User: {userData.Username}, Role: {string.Join(", ", roles.ToArray())}");
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
