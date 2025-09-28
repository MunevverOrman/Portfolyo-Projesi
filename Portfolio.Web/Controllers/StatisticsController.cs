using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{
    public class StatisticsController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.projectCount = context.Projects.Count();
            ViewBag.skillAverage = context.Skills.Any()
                ? context.Skills.Average(x => x.Percentage).ToString("00.00")
                : 0.0.ToString("00.00");

            ViewBag.unreadMessageCount = context.UserMessages.Count(x => x.IsRead == false);
            ViewBag.lastMessageOwner = context.UserMessages
                .OrderByDescending(x => x.UserMessageId)
                .Select(x => x.Name)
                .FirstOrDefault();

            var startYear = context.Experiences.Min(x => x.StartYear);
            ViewBag.experienceYear = DateTime.Now.Year - startYear;

            ViewBag.companyCount = context.Experiences
                .Select(x => x.Company)
                .Distinct()
                .Count();

            ViewBag.reviewAverage = context.Testimonials.Any()
                ? context.Testimonials.Average(x => x.Review).ToString("0.0")
                : "Değerlendirme Yapılmadı";

            ViewBag.maxReviewOwner = context.Testimonials
                .OrderByDescending(x => x.Review)
                .Select(x => x.Name)
                .FirstOrDefault();

          
            ViewBag.trueMessage = context.UserMessages.Where(x => x.IsRead == true).Count();
            ViewBag.falseMessage = context.UserMessages.Where(x => x.IsRead == false).Count();
            ViewBag.educationCount = context.Educations.Count();
            ViewBag.userCount = context.Users.Count();

            return View();
        }
    }
}
