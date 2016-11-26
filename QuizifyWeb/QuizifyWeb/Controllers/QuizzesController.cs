using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers
{
    [RequireHttps, Authorize]
    public class QuizzesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUser CurrentUser => db.Users.Find(User.Identity.GetUserId());

        // GET: Quizzes
        public ActionResult Index()
        {
            var user = CurrentUser;

            var quizzes = new List<Quiz>(db.Quizzes.Where(q => q.Moderator.Id.Equals(user.Id)).ToList());

            foreach (var quiz in db.Quizzes)
            {
                foreach (var t in quiz.Teams)
                {
                    if (t.Users.Contains(user))
                    {
                        quizzes.Add(quiz);
                    }
                }
            }


            return View(quizzes);
        }

        // GET: Quizzes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizzes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizzes/Create
        public ActionResult Create()
        {
            return View();
        }

        public class QuizViewModel
        {
            public int Id { get; set; }
            public string DateTime { get; set; }
            public string Name { get; set; }
            public QuestionVisibility QuestionVisibility { get; set; }
            public bool IsPublic { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string City { get; set; }

            public int QuizCategory { get; set; }
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,DateTime,Name,QuestionVisibility,Location.Latitude,Location.Longitude,Location.City,QuizCategory,IsPublic")]
        public ActionResult Create(QuizViewModel quizModel)
        {
            if (ModelState.IsValid)
            {

                var quiz = new Quiz
                {
                    DateTime = DateTime.Parse(quizModel.DateTime,new CultureInfo("en-US")),
                    IsPublic = quizModel.IsPublic,
                    Location = new Location
                    {
                        City = quizModel.City,
                        Latitude = quizModel.Latitude,
                        Longitude = quizModel.Longitude
                    },
                    Moderator = CurrentUser,
                    Name = quizModel.Name,
                    QuestionVisibility = quizModel.QuestionVisibility,
                    QuizCategory = db.QuizCategories.FirstOrDefault(x => x.Id == quizModel.QuizCategory),
                };


                db.Quizzes.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quizModel);
        }

        // GET: Quizzes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizzes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTime,Name,QuestionVisibility,IsPublic")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        // GET: Quizzes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizzes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizzes.Find(id);
            db.Quizzes.Remove(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}