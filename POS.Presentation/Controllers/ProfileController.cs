using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Attribute;
using POS.Shared;


namespace POS.Presentation.Controllers
{
    [Attribute.Authorize(screen: Constants.CODE_Profile)]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
