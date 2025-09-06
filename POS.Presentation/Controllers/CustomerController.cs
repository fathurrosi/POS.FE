using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Attribute;
using POS.Shared;

namespace POS.Presentation.Controllers
{
    [POSAuthorize(screen: Constants.CODE_Customer)]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
