using Lab4.Data;
using Lab4.ViewModels;

namespace Lab4.Services
{
    public class ReviewService(Db8011Context context) : IReviewService
    {
        private readonly Db8011Context _context = context;

        public HomeViewModel GetHomeViewModel(int numberRows = 20)
        {
            var packs = _context.Packs.Take(numberRows).ToList();
            var allPacks = _context.AllPacks.Take(numberRows).ToList();
            var places = _context.Places.Take(numberRows).ToList();
            var users = _context.Users.Take(numberRows).ToList();
            var placesTypes = _context.PlacesTypes.Take(numberRows).ToList();
            List<UserReview> userReviews = _context.UserReviews.OrderByDescending(l => l.UserLogin).Take(numberRows).ToList();

            HomeViewModel homeViewModel = new()
            {
                Packs = packs,
                AllPacks = allPacks,
                Places = places,
                Users = users,
                PlacesTypes = placesTypes,
                UserReviews = userReviews
            };
            return homeViewModel;
        }
    }
}
