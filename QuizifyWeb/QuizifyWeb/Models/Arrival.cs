using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{
    public class Arrival
    {
        public int Id { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
