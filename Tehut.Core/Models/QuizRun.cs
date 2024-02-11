namespace Tehut.Core.Models
{
    public class QuizRun
    {
        public Quiz? Quiz { get; set; }

        public Dictionary<int, int> UserAnswerPerQuestion { get; } = [];

        public int CurrentQuestionIndex { get; set; }   

        public bool IsCurrentQuestionAnswered => UserAnswerPerQuestion.ContainsKey(CurrentQuestionIndex);

        public static QuizRun CreateFrom(Quiz quiz)
        {
            return new QuizRun
            {
                Quiz = quiz,
                CurrentQuestionIndex = 0,
            };
        }
    }
} 
