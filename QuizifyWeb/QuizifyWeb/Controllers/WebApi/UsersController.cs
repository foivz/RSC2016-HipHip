using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QuizifyWeb.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Owin.Host.SystemWeb;
using System.Data.Entity;

namespace QuizifyWeb.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();


  

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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

        public string Put([FromBody] QuizifyWeb.Common.Users korisnik)
        {
            ApplicationUser user = db.Users.Where(l => l.Email == korisnik.email).FirstOrDefault();

            if(user != null)
            {
                SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            else
            {
                var userr = new ApplicationUser();
                userr.UserName = korisnik.email;
                userr.Email = korisnik.email;
                 
                var registerResult = UserManager.Create(userr, "Default-t-12345");
            

                    SignInManager.SignIn(userr, isPersistent: false, rememberBrowser: false);

                    var dbUser = db.Users.First(t => t.Email == korisnik.email);

                    dbUser.Name = korisnik.ime;
                    db.SaveChanges();

                return dbUser.Id;
            }
            return "error";
           
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