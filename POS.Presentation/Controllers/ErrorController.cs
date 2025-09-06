using Microsoft.AspNetCore.Mvc;

namespace POS.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            //if (statusCode == 401)
            //{
            //    return View("401");
            //}
            //else if (statusCode == 404)
            //{
            //    return View("404");
            //}
            //else if (statusCode == 400)
            //{
            //    return View("400");
            //}
            //else if (statusCode == 401)
            //{
            //    return View("401");
            //}
            //else if (statusCode == 403)
            //{
            //    return View("403");
            //}

            //else if (statusCode == 500)
            //{
            //    return View("500");
            //}

            return View(statusCode.ToString());
        }
    }
}
