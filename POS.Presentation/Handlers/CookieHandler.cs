using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Presentation.Handlers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Newtonsoft.Json;
    using POS.Domain.Entities;    
    using System;
    using System.ComponentModel.Design;
    using System.Security.Claims;

    public class CookieHandler : CookieAuthenticationEvents
    {
        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }

        public override Task SigningIn(CookieSigningInContext context)
        {
            return base.SigningIn(context);
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            //if (context.Principal?.Identity?.IsAuthenticated == false)
            //{
            //    return Task.CompletedTask;
            //}

            if (context.Principal?.Identity?.IsAuthenticated == false)
            {
                context.RejectPrincipal();
                context.ShouldRenew = true; // This might trigger a new authentication request
                return Task.CompletedTask;
            }
            

            //string? userName = context.Principal?.Identity?.Name;
            //if (context.HttpContext.Request.Cookies.TryGetValue("UserData", out string protectedUserData))
            //{
            //    UserData? userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
            //    if (userData != null && userData.Previllages.Count() > 0)
            //    {
            //        // Example: Log the username
            //        Console.WriteLine($"User: {userData.Username}, Role: {string.Join(", ", userData.Roles.ToArray())}");

            //    }
            //    else
            //    {
            //        //context.RejectPrincipal();
            //    }
            //}
            return base.ValidatePrincipal(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }
    }
}
