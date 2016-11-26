using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{
    public class Team
    {
        public int Id { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
        public string Name { get; set; }

        public Team()
        {
            Users = new List<ApplicationUser>();

        }
    }
}
