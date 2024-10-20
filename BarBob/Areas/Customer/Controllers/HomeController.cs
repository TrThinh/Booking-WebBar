using BarBob.Models.ViewModels;
using BarBob.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var recentFeedbacks = _unitOfWork.Feedback.GetAll(includeProperties: "User")
                .OrderByDescending(f => f.FeedbackDate)
                .Take(3)
                .Select(f => new FeedbackVM
                {
                    FullName = f.User.FirstName + " " + f.User.LastName,
                    Title = f.Title,
                    Status = f.Status,
                    FeedbackDate = f.FeedbackDate,
                    Images = f.Images
                })
                .ToList();

            var allFeedbacks = _unitOfWork.Feedback.GetAll(includeProperties: "User")
                .OrderByDescending(f => f.FeedbackDate)
                .Select(f => new FeedbackVM
                {
                    FullName = f.User.FirstName + " " + f.User.LastName,
                    Title = f.Title,
                    Status = f.Status,
                    FeedbackDate = f.FeedbackDate,
                    Images = f.Images
                })
                .ToList();

            var feedbackModel = new FeedbackListVM
            {
                RecentFeedbacks = recentFeedbacks,
                AllFeedbacks = allFeedbacks
            };

            return View(feedbackModel);
        }

        public IActionResult OurEvent()
        {
            return View();
        }

        public IActionResult BookEvent()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }

}
