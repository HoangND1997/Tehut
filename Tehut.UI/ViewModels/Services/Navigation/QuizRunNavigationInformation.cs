using Tehut.Core.Models;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    internal class QuizRunNavigationInformation : NavigationInformation
    {
        public QuizRun? Run { get; set; }

        public int CurrentQuestionIndex { get; set; }

        public QuizRunNavigationInformation GetNextQuestion()
        { 
            return new QuizRunNavigationInformation
            {
                Run = Run,
                CurrentQuestionIndex = CurrentQuestionIndex + 1
            };
        }

        public bool HasNextQuestion() => Run?.Quiz?.Questions.Count > CurrentQuestionIndex + 1; 
    }
}
