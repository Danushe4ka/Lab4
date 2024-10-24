using Lab4.ViewModels;

namespace Lab4.Services
{
    public interface IReviewService
    {
        HomeViewModel GetHomeViewModel(int numberRows = 20);
    }
}
