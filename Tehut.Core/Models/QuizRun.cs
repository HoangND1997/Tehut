namespace Tehut.Core.Models
{
    public class QuizRun
    {
        public Quiz? Quiz { get; set; }

        public Dictionary<int, int> UserAnswerPerQuestion { get; } = [];


        public bool IsCurrentQuestionAnswered(int currentQuestionIndex) => UserAnswerPerQuestion.ContainsKey(currentQuestionIndex);

        public bool IsQuizFinished() => UserAnswerPerQuestion.Count == Quiz?.Questions.Count;   

        public List<int> GetCorrectlyAnsweredQuestions() => UserAnswerPerQuestion.Keys.Where(questionIndex => Quiz?.Questions[questionIndex].CorrectAnswer == UserAnswerPerQuestion[questionIndex]).ToList();

        public List<int> GetInCorrectlyAnsweredQuestions() => UserAnswerPerQuestion.Keys.Where(questionIndex => UserAnswerPerQuestion[questionIndex] != -1 && Quiz?.Questions[questionIndex].CorrectAnswer != UserAnswerPerQuestion[questionIndex]).ToList();

        public List<int> GetSkippedQuestions() => UserAnswerPerQuestion.Keys.Where(questionIndex => UserAnswerPerQuestion[questionIndex] == -1).ToList();


        public static QuizRun CreateFrom(Quiz quiz)
        {
            return new QuizRun
            {
                Quiz = quiz,
            };
        }
    }
}
