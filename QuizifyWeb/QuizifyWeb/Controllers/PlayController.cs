using System.Web.Mvc;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int id)
        {

            var quiz = db.Quizzes.Find(id);

            return View(quiz);
        }
    }
}