namespace Tehut.Core.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        public string Question { get; set; } = string.Empty; 


        public Quiz? Quiz { get; set; } = null!;


        public string Answer1 { get; set; } = string.Empty; 

        public string Answer2 { get; set; } = string.Empty;

        public string Answer3 { get; set; } = string.Empty;
        
        public string Answer4 { get; set; } = string.Empty;


        public int CorrectAnswer { get; private set; }

        public void SetCorrectAnswer(int answerId)
        {
            if (answerId < 0 || answerId > 3)
            {
                throw new ArgumentException("Only answers from 0 to 3 can be chosen as correct!"); 
            }

            CorrectAnswer = answerId;
        }
    }
}
