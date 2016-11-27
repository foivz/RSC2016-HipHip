using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers.WebApi
{
    public class QuizzesStatController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Get(int id)
        {
            var q = db.Quizzes.Find(id);
            return Ok(new { DateTime = q.DateTimeStarted.Year == 1 ? "1" : q.DateTimeStarted.ToString("HH:mm:ss")});
        }

        //[HttpPost]
        //public IHttpActionResult Post([FromBody])
        //{


        //    return Ok();
        //}
    }
}
