using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using POS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared
{
    public class Utilities
    {
        public static UserData? GetLoginInfo(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string username = string.Format("{0}", context.User.Identity?.Name);
                string protectedUserData = context.Session.GetString($"UserData_{username}");
                UserData? userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
                return userData;
            }
            return null;
        }

    }
}
