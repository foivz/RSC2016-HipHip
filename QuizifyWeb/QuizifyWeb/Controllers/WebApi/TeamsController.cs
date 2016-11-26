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
    public class TeamsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Teams
        public IQueryable<Team> GetTeams()
        {
            return db.Teams;
        }

        // GET: api/Teams/5
        [ResponseType(typeof(Team))]
        public IHttpActionResult GetTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        public class AddUserViewModel
        {
            public string Email { get; set; }

            public int TeamId { get; set; }
        }

        public class SearchUserViewModel
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
        }

        [ResponseType(typeof(ApplicationUser)),HttpPut]
        public IHttpActionResult PutApplicationUserToTeam(AddUserViewModel addUserViewModel)
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

                    return Ok(new SearchUserViewModel
                    {
                      Email  = user.Email,
                      Name = user.Name,
                      Id = user.Id
                    });

                }
                else
                {
                    return Conflict();
                }


            }

            return BadRequest("No user found with that email!");
        }

        // POST: api/Teams
        [ResponseType(typeof(Team))]
        public IHttpActionResult PostTeam(TeamViewModel teamModel)
        {
         


            var team = db.Teams.Add(new Team
            {
                Name = teamModel.Name
            });

            db.SaveChanges();


            teamModel.UserIds.ForEach(id =>
            {
                var user = db.Users.Find(id);

                team.Users.Add(user);
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Teams/5
        [ResponseType(typeof(Team))]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.Id == id) > 0;
        }
    }

    public class TeamViewModel
    {
        public  ICollection<string> UserIds { get; set; }
        public string Name { get; set; }

    }
}