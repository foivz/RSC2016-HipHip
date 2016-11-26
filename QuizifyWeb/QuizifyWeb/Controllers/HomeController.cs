using System.Linq;
using System.Web.Mvc;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Quizzes.Where(q => q.IsPublic).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Quizzes()
        {
            ViewBag.Message = "Quizzess";

            return View(db.Quizzes.Where(q => q.IsPublic).ToList());
        }
    }
}