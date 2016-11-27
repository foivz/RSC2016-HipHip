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
    public class qIdK
    {
        public int idKviz { get; set; }
        public string idKor { get; set; }
    }

    public class pitanje
    {
        public string pitanj { get; set; }
        public int tip { get; set; }
        public List<odgovori> odgovori { get; set; }
    }

    public class odgovori
    {
        public int id { get; set; }
        public string odgovor { get; set; }
    }

    public class apiOdgovor
    {
        public int idPitanja { get; set; }
        public int idOdgovora { get; set; }
        public string idKorisnika { get; set; }
        public string text { get; set; }
    }

    public class StartController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Start/")]
        public pitanje Post([FromBody] qIdK q)
        {
            var kviz = db.Quizzes.FirstOrDefault(l => l.Id == q.idKviz);

            if (kviz.DateTimeStarted.Year > 1)
            {
                var pitanje = db.Questions.First(l => l.Quiz.Id == kviz.Id && l.IsCurrent);
                List<odgovori> odgovori = new List<odgovori>();
                var odgovor = db.Answers.Where(l => l.Question.Id == pitanje.Id).ToList();

                foreach (Answer a in odgovor)
                {
                    odgovori.Add(new odgovori() {id = a.Id, odgovor = a.Text});
                }





                return new pitanje()
                {
                    pitanj = pitanje.Text,
                    tip = (int) pitanje.QuestionType,
                    odgovori = odgovori
                };
            }

            return new pitanje()
            {
                pitanj = "0",
                tip = 0,
                odgovori = null
            };
        }

        [Route("api/Start/Add")]
        public void Put([FromBody] apiOdgovor q)
        {
            var odgovor = db.Answers.Find(q.idOdgovora);
            var pitanje = db.Questions.Find(q.idPitanja);

            var kviz = db.Quizzes.First(x => x.Id == pitanje.Quiz.Id);

            var useraw = new UserAnswer()
            {
                Question = pitanje,
                Quiz = kviz,
                Answer = odgovor,
                Text = q.text
            };

            db.UserAnswers.Add(useraw);

            db.SaveChanges();
        }
    }
}