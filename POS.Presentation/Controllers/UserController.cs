using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Common;
using POS.Presentation.Handlers;
using POS.Domain.Entities;
using POS.Domain.Models;
using POS.Domain.Models.Response;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using System.Security.Claims;

namespace POS.Presentation.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IAuthService _authService;
        private IPrevillageService _previllageService;
        private IDistributedCache _cache;
        public UserController(IUserService userService,
            IRoleService roleService,
            IPrevillageService previllageService,
            IDistributedCache cache,
            IAuthService authService)
        {
            _userService = userService;
            _roleService = roleService;
            _previllageService = previllageService;
            //  _menuService = menuService;
            _cache = cache;
            _authService = authService;
        }


        public async Task<IActionResult> Index()
        {
            List<User> items = await _userService.GetDataAsync();
            return View(items);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                LoginResponse<User> response = await _authService.GetTokenFromApiAsync(new Domain.Models.Request.LoginRequest
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

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(60),
                    };

                    Response.Cookies.Append("token", response.Token, cookieOptions);

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }

    }
}
