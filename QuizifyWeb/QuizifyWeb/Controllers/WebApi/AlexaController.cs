using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuizifyWeb.Models;
using WebGrease.Css.Extensions;

namespace QuizifyWeb.Controllers.WebApi
{
    public class qId
    {
        public string Id { get; set; }
    }
    public class AlexaController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Alex/Start")]
        public void Post([FromBody] qId q)
        {
            var quiz = db.Quizzes.Where(l => l.Id == int.Parse(q.Id)).FirstOrDefault();

            quiz.DateTimeStarted = (DateTimeOffset.Now).AddSeconds(5);

            db.SaveChanges();

        }
    }
}