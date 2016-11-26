using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizifyWeb.Models
{

    public enum QuestionType
    {
        MultipleAnswers,
        SingleAnswer,
        Text
    };
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Category Category { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
