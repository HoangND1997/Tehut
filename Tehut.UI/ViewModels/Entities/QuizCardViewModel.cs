using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuizCardViewModel
    {
        private readonly Quiz quiz;
        private readonly Services.Navigation.INavigationService navigationService;

        public string Name { get; }

        public int QuestionCount { get; }

        
        public AsyncCommand RunQuizCommand { get; }

        public AsyncCommand EditQuizCommand { get; }

        public AsyncCommand DeleteQuizCommand { get; }

        
        public QuizCardViewModel(Quiz quiz, Services.Navigation.INavigationService navigationService) 
        {
            Name = quiz.Name;
            QuestionCount = quiz.Questions.Count; 

            RunQuizCommand = new AsyncCommand(RunQuiz);
            EditQuizCommand = new AsyncCommand(EditQuiz);
            DeleteQuizCommand = new AsyncCommand(DeleteQuiz);

            this.quiz = quiz;
            this.navigationService = navigationService;
        }

        private async Task RunQuiz()
        {

        }

        private async Task EditQuiz()
        {
            await navigationService.NavigateTo<QuizEditViewModel>(new QuizEditNavigationInformation { QuizToEdit = quiz }); 
        }

        private async Task DeleteQuiz()
        { 
        
        }
    }
}
