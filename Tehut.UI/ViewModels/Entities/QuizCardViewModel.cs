using DevExpress.Mvvm;
using Tehut.Core.Models;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuizCardViewModel
    {
        private readonly Services.Navigation.INavigationService navigationService;

        public string Name { get; }

        public int QuestionCount { get; }

        
        public DelegateCommand RunQuizCommand { get; }

        public DelegateCommand EditQuizCommand { get; }

        public DelegateCommand DeleteQuizCommand { get; }

        
        public QuizCardViewModel(Quiz quiz, Services.Navigation.INavigationService navigationService) 
        {
            Name = quiz.Name;
            QuestionCount = quiz.Questions.Count; 

            RunQuizCommand = new DelegateCommand(RunQuiz);
            EditQuizCommand = new DelegateCommand(EditQuiz);
            DeleteQuizCommand = new DelegateCommand(DeleteQuiz);
            this.navigationService = navigationService;
        }

        private void RunQuiz()
        {

        }

        private void EditQuiz()
        {
            navigationService?.NavigateTo<QuizEditViewModel>(); 
        }

        private void DeleteQuiz()
        { 
        
        }
    }
}
