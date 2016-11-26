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

namespace QuizifyWeb.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Users
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        public class SearchUserViewModel
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
        }


       

        // POST: api/Users
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(SearchUserViewModel searchUserViewModel)
        {
            var email = searchUserViewModel.Email;
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(email));

            if (user != null)
            {
                return Ok(new SearchUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                });
            }

            return BadRequest("No user found with that email!");
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PutApplicationUser(WebApi.TeamsController.AddUserViewModel addUserViewModel)
        {
            var email = addUserViewModel.Email;
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(email));
            var team = db.Teams.FirstOrDefault(t => t.Id == addUserViewModel.TeamId);

            if (user != null && team != null)
            {

                if (team.Users.Contains(user) == false)
                {
                    team.Users.Add(user);

                    db.SaveChanges();

                    return Ok(user);

                }
                else
                {
                    return Conflict();
                }


            }

            return BadRequest("No user found with that email!");
        }
    }
}