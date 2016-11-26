using System;
using System.Collections.Generic;

namespace QuizifyWeb.Models
{
    public enum QuestionVisibility
    {
        Public,
        Protected
    }

    public class Quiz
    {
        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public QuestionVisibility QuestionVisibility { get; set; }
        public bool IsPublic { get; set; }
        public virtual ApplicationUser Moderator { get; set; }
        public virtual List<Team> Teams { get; set; }
        public virtual Location Location { get; set; }
        public virtual QuizCategory QuizCategory { get; set; }
    }
}