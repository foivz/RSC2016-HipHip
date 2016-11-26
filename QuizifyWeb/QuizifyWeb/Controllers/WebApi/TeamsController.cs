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
    public class Timovi
    {
        public int id { get; set; }
        public string imeTima { get; set; }
    }

    public class Members
    {
        public string ime { get; set; }
    }

    public class Korisnik
    {
        public string id { get; set; }
    }

    public class TeamsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Teams
        public IQueryable<Team> GetTeams()
        {
            return db.Teams;
        }

        [Route("api/Teams/My")]
        public List<Timovi> Post([FromBody] Korisnik korisnik)
        {
            List<Timovi> timovi = new List<Timovi>();
            ApplicationUser dbUser = db.Users.Where(l => l.Id == korisnik.id).FirstOrDefault();

            foreach (var team in db.Teams.ToList())
            {
                if (team.Users.Contains(dbUser))
                {
                    Timovi temp = new Timovi() {
                        id = team.Id,
                        imeTima = team.Name
                    };
                    timovi.Add(temp);
                }
            }

            if(timovi.Count() == 0)
            {
                timovi.Add(new Timovi()
                {
                    id = 0,
                    imeTima = "error"
                });
            }

            return timovi;
        }

        [Route("api/Teams/Members")]
        public List<Members> Post([FromBody] Timovi timovi)
        {
            List<Members> clanovi = new List<Members>();
            var team = db.Teams.Where(l => l.Id == timovi.id).FirstOrDefault();

            foreach (var kor in team.Users)
            {
                if (kor.Team.Contains(team))
                {
                    Members temp = new Members()
                    {
                        ime = kor.Name
                    };
                    clanovi.Add(temp);
                }
            }

            if (clanovi.Count == 0)
            {
                clanovi.Add(new Members()
                {
                   ime = "error"
                });
            }

            return clanovi;
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