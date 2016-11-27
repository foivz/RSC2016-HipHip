namespace QuizifyWeb.Models
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
