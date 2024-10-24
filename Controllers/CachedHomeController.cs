using Lab4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lab4.Controllers
{
    // Выборка кэшированых данных из IMemoryCache
    public class CachedHomeController(IMemoryCache memoryCache) : Controller
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public IActionResult Index()
        {
            //считывание данных из кэша
            HomeViewModel homeViewModel = _memoryCache.Get<HomeViewModel>("Test 20");
            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }
    }
}
