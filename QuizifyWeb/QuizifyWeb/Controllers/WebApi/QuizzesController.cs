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
    public class QuizBasic
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class uID
    {
        public string id { get; set; }
    }

    public class QuizzesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Quizzez/My")]
        public List<QuizBasic> Post([FromBody]uID id)
        {
            var user = db.Users.Where(l => l.Id == id.id).FirstOrDefault();
            var kvizovi = db.Quizzes.ToList();

            List<QuizBasic> lista = new List<QuizBasic>();

            foreach (var quiz in db.Quizzes)
            {
                if (quiz.Teams.Any(x => x.Users.Any(s => s.Id == user.Id)))
                {

                    lista.Add(new QuizBasic() { id = quiz.Id, name = quiz.Name });
                    }
                
            }

            if(lista.Count == 0)
            {
                lista.Add(new QuizBasic() { id = 0, name = "empty" });
            }

            return lista;
        }

        public IHttpActionResult GetUsersArrival(string id)
        {
            return Ok(new {isPresent = db.Users.Find(id).IsPresent});
        }



        
    }
}