using Tehut.Core.Models;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuizCardViewModel
    {
        public string Name { get; }

        public int QuestionCount { get; }

        public QuizCardViewModel(Quiz quiz) 
        {
            Name = quiz.Name;
            QuestionCount = 18; 
        }
    }
}
