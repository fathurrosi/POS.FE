using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Presentation.Models;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using System.Collections.Generic;

namespace POS.Presentation.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly IDistributedCache _cache;
        public MenuViewComponent(IMenuService menuService, IDistributedCache cache)
        {
            _menuService = menuService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {            
            List<Menu> items = await GetMenusAsync();
            if (items == null)
            {
                // Get menu data from database or elsewhere
                items = await _menuService.GetDataAsync();
                StoreCacheAsync(items);
            }
            return View(items);

        }


        public async Task<List<Menu>?> GetMenusAsync()
        {
            var dataJson = await _cache.GetStringAsync("MENUS");
            if (dataJson != null)
            {
                return JsonConvert.DeserializeObject<List<Menu>>(dataJson);
            }
            return null;
        }




        public async Task StoreCacheAsync(List<Menu> menus)
        {
            TimeSpan timeSpan = TimeSpan.FromMinutes(60);
            DateTime expirationDate = DateTime.UtcNow.Add(timeSpan);
            var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan); // expires in 1 hour

            var dataJson = JsonConvert.SerializeObject(menus);
            await _cache.SetStringAsync("MENUS", dataJson, cacheEntryOptions);
        }

    }
}
