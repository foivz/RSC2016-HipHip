using System.Web.Http;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers.WebApi
{
    public class RollerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Get(int id)
        {
            foreach (var dbQuestion in db.Questions)
            {
                dbQuestion.IsCurrent = false;
            }


            var q = db.Questions.Find(id);


            q.IsCurrent = true;

            db.SaveChanges();

            return Ok();
        }
    }
}