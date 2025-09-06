using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS.Presentation.Attribute;
using POS.Presentation.Models;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Settings;

namespace POS.Presentation.Controllers
{
    [POSAuthorize(screen: Constants.CODE_Menu)]
    public class MenuController : Controller
    {
        private readonly PagingSettings _pagingSettings;
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService, PagingSettings pagingSettings)
        {
            _menuService = menuService;
            _pagingSettings = pagingSettings;
        }

        public async Task<IActionResult> DisplayManu()
        {
            var menuItems = await _menuService.GetDataAsync();
            return View("~/Views/Shared/_MenuPartial.cshtml", menuItems);
        }

        public async Task<IActionResult> Index(int? pageIndex = 1)
        {
            var result = await _menuService.GetPagingAsync(pageIndex.Value, _pagingSettings.DefaultPageSize);

            return View(new MenuListModel(result));
        }


        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return await Edit(id);
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _menuService.GetDataByIdAsync(id);
            var menuItems = await _menuService.GetDataAsync();
            var model = new MenuModel(result);
            model.ParentList = menuItems.Where(t => t.Id != id).Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _menuService.Save(model.Item);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Important for security
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemToDelete = await _menuService.Delete(id);
            if (itemToDelete == 0)
            {
                return NotFound(); // Handle case where item is not found
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: ProductController/Edit/5
        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await _menuService.Delete(id);
            if (itemToDelete == 0)
            {
                return NotFound(); 
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
