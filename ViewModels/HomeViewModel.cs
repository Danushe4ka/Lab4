using Lab4.Models;
using Microsoft.Identity.Client;

namespace Lab4.ViewModels
{
    public class HomeViewModel
    {
        public required IEnumerable<Pack> Packs { get; set; }
        public required IEnumerable<AllPack>? AllPacks { get; set; }
        public required IEnumerable<Place>? Places { get; set; }
        public required IEnumerable<User>? Users { get; set; }
        public required IEnumerable<PlacesType> PlacesTypes { get; set; }
        public required IEnumerable<UserReview> UserReviews { get; set; }
    }
}
