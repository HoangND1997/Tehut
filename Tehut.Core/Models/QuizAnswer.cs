namespace Tehut.Core.Models
{
    public class QuizAnswer
    {
        public int Id { get; set; }

        public string Answer { get; set; } = string.Empty;

        public int QuestionId { get; set; }

        public QuizQuestion? Question { get; set; } = null!;
    }
}
