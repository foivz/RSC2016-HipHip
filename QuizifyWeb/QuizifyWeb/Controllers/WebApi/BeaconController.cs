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
    public class BeaconData
    {
        public string id { get; set; }
        public bool prisutan { get; set; }
    }
    public class BeaconController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Beacon/Here")]
        public void Post([FromBody] BeaconData b)
        {
            var user = db.Users.Where(l => l.Id == b.id).FirstOrDefault();

            user.IsPresent = b.prisutan;

            //db.Users.Add(user);
            db.SaveChanges();

        }
    }
}