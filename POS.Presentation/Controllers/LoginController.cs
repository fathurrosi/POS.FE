using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Models;
using POS.Domain.Models.Response;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using POS.Shared.Settings;
using System.Security.Claims;

namespace POS.Presentation.Controllers
{
    public class LoginController : Controller
    {

        //private readonly PagingSettings _pagingSettings;
        //private readonly IUserService _userService;
        //private readonly IRoleService _roleService;
        private readonly IAuthService _authService;
        //private readonly IPrevillageService _previllageService;
        //private readonly IDistributedCache _cache;
        public LoginController(IAuthService authService)
        {
            //_userService = userService;
            //_roleService = roleService;
            //_previllageService = previllageService;

            //_pagingSettings = pagingSettings;
            //_cache = cache;
            _authService = authService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                LoginResponse<User> response = await _authService.LoginAsync(new Domain.Models.Request.LoginRequest
                {
                    Username = model.Username,
                    Password = model.Password
                });
                if (response != null && response.Success)
                {
                    User user = response.Data;
                    List<Role> roles = response.Roles;
                    List<VUserPrevillage> previllages = response.Previllages;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                    };

                    roles.ForEach(t =>
                    {
                        claims.Add(new Claim(ClaimTypes.Role, t.Name));
                    });


                    var identity = new ClaimsIdentity(claims, POS.Shared.Constants.Cookies_Name);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    var userData = new UserData { Username = model.Username, Roles = roles.Select(t => t.Name).ToList(), Previllages = previllages };
                    var userDataJson = JsonConvert.SerializeObject(userData);
                    var base64data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userDataJson));
                   
                    HttpContext.Session.SetString($"UserData_{model.Username}", base64data);

                    //var cookieOptions = new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = true,
                    //    SameSite = SameSiteMode.Strict,
                    //    Expires = DateTime.UtcNow.AddMinutes(60),
                    //};

                    //Response.Cookies.Append("token", response.Token, cookieOptions);

                    //await StoreTokenAsync(new RefreshTokenResponse { AccessToken = response.Token, RefreshToken = response.RefreshToken });

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }

            return View(model);
        }

    }
}
