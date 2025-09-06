using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Models;
using POS.Shared;
using System.Diagnostics;

namespace POS.Presentation.Controllers
{
    //[POSAuthorize("Admin,User")]

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[POSAuthorize("Admin,User")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignOut()
        {   
            await HttpContext.SignOutAsync(Constants.Cookies_Name);
            //Response.Cookies.Delete("UserData");
            return RedirectToAction("Login", "User");
        }
    }
}
