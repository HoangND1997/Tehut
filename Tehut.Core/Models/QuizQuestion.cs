namespace Tehut.Core.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        public string Question { get; set; } = string.Empty; 

        public int QuizId { get; set; }

        public Quiz? Quiz { get; set; } = null!;
        

        public List<string> Answers { get; set; } = new();

        public int CorrectAnswerId { get; set; }

        public QuizAnswer? CorrectAnswer { get; set; } = null!; 
    }
}
