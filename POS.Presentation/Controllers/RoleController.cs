using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entities;
using POS.Presentation.Attribute;
using POS.Presentation.Models;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Settings;


namespace POS.Presentation.Controllers
{
    [POSAuthorize(screen: Constants.CODE_Role)]
    public class RoleController : Controller
    {


        private readonly PagingSettings _pagingSettings;
        private readonly IRoleService _roleService;
        protected IAuthorizationService _authorizationService { get; }
        public RoleController(IRoleService RoleService, IAuthorizationService authorizationService, PagingSettings pagingSettings)
        {
            _roleService = RoleService;
            _authorizationService = authorizationService;
            _pagingSettings = pagingSettings;
        }
        // GET: RoleController


        public async Task<IActionResult> Index(int? pageIndex = 1)
        {
            var result = await _roleService.GetPagingAsync(pageIndex.Value, _pagingSettings.DefaultPageSize);

            return View(new RoleListModel(result));
        }
        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _roleService.GetDataByIdAsync(id);
            var RoleItems = await _roleService.GetDataAsync();
            var model = new RoleModel(result);
            //model.ParentList = RoleItems.Where(t => t.Id != id).Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _roleService.Save(model.Item);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }


        // GET: ProductController/Edit/5
        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await _roleService.Delete(id);
            if (itemToDelete == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Create()
        {
            var item = new RoleModel();
            item.IsDisabled = false;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _roleService.Save(model.Item);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
    }
}
