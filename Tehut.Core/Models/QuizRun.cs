namespace Tehut.Core.Models
{
    public class QuizRun
    {
        public Quiz? Quiz { get; set; }

        public Dictionary<int, int> UserAnswerPerQuestion { get; } = [];

        public bool IsCurrentQuestionAnswered(int currentQuestionIndex) => UserAnswerPerQuestion.ContainsKey(currentQuestionIndex);

        public static QuizRun CreateFrom(Quiz quiz)
        {
            return new QuizRun
            {
                Quiz = quiz,
            };
        }
    }
}
