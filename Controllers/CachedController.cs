using Lab4.Data;
using Lab4.Infrastructure.Filters;
using Lab4.Models;
using Lab4.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab4.Controllers
{
    public class CachedController(Db8011Context context) : Controller
    {
       private readonly Db8011Context _context = context;

        // Кэширование с использования фильтра ресурсов
        [TypeFilter(typeof(CacheResourceFilterAttribute))]
        public IActionResult Index()
        {
            int numberRows = 20;
            List<Pack> packs = [.. _context.Packs.Take(numberRows)];
            List<AllPack> allPacks = [.. _context.AllPacks.Take(numberRows)];
            List<Place> places = [.. _context.Places.Take(numberRows)];
            List<User> users = [.. _context.Users.Take(numberRows)];
            List<PlacesType> placesTypes = [.. _context.PlacesTypes.Take(numberRows)];
            List<UserReview> userReviews = [.. _context.UserReviews.OrderByDescending(l => l.UserLogin).Take(numberRows)];

            HomeViewModel homeViewModel = new()
            {
                Packs = packs,
                AllPacks = allPacks,
                Places = places,
                Users = users,
                PlacesTypes = placesTypes,
                UserReviews = userReviews
            };
            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }
    }
}
