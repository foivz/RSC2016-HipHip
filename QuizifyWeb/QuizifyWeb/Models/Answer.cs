using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
