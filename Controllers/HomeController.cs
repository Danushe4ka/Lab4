using Lab4.Data;
using Lab4.Infrastructure.Filters;
using Lab4.Models;
using Lab4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lab4.Controllers
{
    [Infrastructure.Filters.ExceptionFilter]
    [TypeFilter(typeof(TimingLogAttribute))]
    public class HomeController(Db8011Context db) : Controller
    {
        private readonly Db8011Context _db = db;

        public IActionResult Index()
        {
            int numberRows = 20;
            List<Pack> packs = [.. _db.Packs.Take(numberRows)];
            List<AllPack> allPacks = [.. _db.AllPacks.Take(numberRows)];
            List<Place> places = [.. _db.Places.Take(numberRows)];
            List<User> users = [.. _db.Users.Take(numberRows)];
            List<PlacesType> placesTypes = [.. _db.PlacesTypes.Take(numberRows)];
            List<UserReview> userReviews = [.. _db.UserReviews.OrderByDescending(l => l.UserLogin).Take(numberRows)];

            HomeViewModel homeViewModel = new()
            {
                Packs = packs,
                AllPacks = allPacks,
                Places = places,
                Users = users,
                PlacesTypes = placesTypes,
                UserReviews = userReviews
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
