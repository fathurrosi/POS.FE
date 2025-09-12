using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Common;
using POS.Domain.Entities;
using POS.Domain.Models;
using POS.Domain.Models.Response;
using POS.Presentation.Handlers;
using POS.Presentation.Models;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Settings;
using System.Security.Claims;

namespace POS.Presentation.Controllers
{
    public class UserController : Controller
    {

        private readonly PagingSettings _pagingSettings;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAuthService _authService;
        private readonly IPrevillageService _previllageService;
        private readonly IDistributedCache _cache;
        public UserController(IUserService userService,
            IRoleService roleService,
            IPrevillageService previllageService,
            IDistributedCache cache,
            PagingSettings pagingSettings,
            IAuthService authService)
        {
            _userService = userService;
            _roleService = roleService;
            _previllageService = previllageService;

            _pagingSettings = pagingSettings;
            _cache = cache;
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

                    var userData = new UserData { 
                        Username = model.Username, 
                        Roles = roles.Select(t => t.Name).ToList(), 
                        Previllages = previllages ,
                        Token = new JwtToken { AccessToken = response.Token, RefreshToken = response.RefreshToken, RefreshTokenExpires = user.RefreshTokenExpires.GetValueOrDefault() } 
                    };
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

                    //await StoreTokenAsync(new RefreshTokenResponse { AccessToken = response.Token, RefreshToken = response.RefreshToken, RefreshTokenExpires = user.RefreshTokenExpires.GetValueOrDefault() });

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


        public async Task<IActionResult> Index(int? pageIndex = 1)
        {
            var result = await _userService.GetPagingAsync(pageIndex.Value, _pagingSettings.DefaultPageSize);

            return View(new UserListModel(result));
        }


        //public async Task StoreTokenAsync(RefreshTokenResponse refreshToken)
        //{
        //    TimeSpan timeSpan = TimeSpan.FromMinutes(2);
        //    DateTime expirationDate = DateTime.UtcNow.Add(timeSpan);
        //    var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan); // expires in 1 hour

        //    var tokenJson = JsonConvert.SerializeObject(refreshToken);
        //    await _cache.SetStringAsync("POSToken", tokenJson, cacheEntryOptions);
        //}
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    Response.Cookies.Delete("token");
        //    return RedirectToAction("Login", "User");
        //}

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Active", Value = true.ToString() });
            selectList.Add(new SelectListItem { Text = "Inactive", Value = false.ToString() });

            var result = await _userService.GetById(id);
            var model = new UserModel(result);
            model.StatusList = selectList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.Save(model.Item);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }


        public IActionResult Create()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "Active", Value = true.ToString() });
            selectList.Add(new SelectListItem { Text = "Inactive", Value = false.ToString() });

            var model = new UserModel();
            model.StatusList = selectList;
            model.IsDisabled = false;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.Save(model.Item);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(string id)
        {
            var itemToDelete = await _userService.Delete(id);
            if (itemToDelete == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
