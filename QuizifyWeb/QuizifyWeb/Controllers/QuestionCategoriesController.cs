using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using QuizifyWeb.Models;

namespace QuizifyWeb.Controllers
{
    public class QuestionCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionCategories
        public ActionResult Index(int id)
        {
            return View(db.QuestionCategories.Where(x => x.Quiz.Id == id).ToList());
        }

        // GET: QuestionCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = db.QuestionCategories.Find(id);
            if (questionCategory == null)
            {
                return HttpNotFound();
            }
            return View(questionCategory);
        }

        // GET: QuestionCategories/Create
        public ActionResult Create(int id)
        {
            return View(new QuestionCategory
            {
                Quiz = db.Quizzes.Find(id)
            });
        }

        // POST: QuestionCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name")]
        public ActionResult Create([FromBody] string name, int quizId)
        {
            var questionCategory = new QuestionCategory
            {
                Name = name,
                Quiz = db.Quizzes.Find(quizId)
            };

            if (ModelState.IsValid)
            {
                db.QuestionCategories.Add(questionCategory);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = quizId});
            }

            return View(questionCategory);
        }

        // GET: QuestionCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = db.QuestionCategories.Find(id);
            if (questionCategory == null)
            {
                return RedirectToAction("Index", "Quizzes");
            }
            return View(questionCategory);
        }

        // POST: QuestionCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] QuestionCategory questionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Quizzes");

        }

        // GET: QuestionCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategory questionCategory = db.QuestionCategories.Find(id);
            if (questionCategory == null)
            {
                return HttpNotFound();

            }
            return View(questionCategory);
        }

        // POST: QuestionCategories/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionCategory questionCategory = db.QuestionCategories.Find(id);

            
            foreach (var question in db.Questions.Where(x => x.QuestionCategory.Id == id))
            {
                db.Questions.Remove(question);
            }

            db.QuestionCategories.Remove(questionCategory);

            db.SaveChanges();
            return RedirectToAction("Index", "Quizzes");

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