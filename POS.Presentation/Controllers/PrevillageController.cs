using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Attribute;
using POS.Shared;

namespace POS.Presentation.Controllers
{
    [Authorize(screen: Constants.CODE_Previllage)]
    public class PrevillageController : Controller
    {
        // GET: PrevillageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrevillageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrevillageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrevillageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrevillageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrevillageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrevillageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrevillageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
