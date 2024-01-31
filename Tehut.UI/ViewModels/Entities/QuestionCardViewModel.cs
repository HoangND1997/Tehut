using DevExpress.Mvvm;
using Tehut.Core.Models;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuestionCardViewModel : ViewModelBase
    {
        public string QuestionText { get; set; }

        public List<string> Answers { get; set; }

        public QuestionCardViewModel(QuizQuestion question) 
        {
            QuestionText = question.Question; 

            Answers = question.Answers.Select((a, i) => $"{i+1}. {a}").ToList();
        }  
    }
}
