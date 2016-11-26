using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        public virtual Quiz Quiz { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public virtual Answer Answer { get; set; }
        public string Text { get; set; }
    }
}
