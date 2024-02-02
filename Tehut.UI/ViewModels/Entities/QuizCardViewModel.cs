using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Messages;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuizCardViewModel
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IQuizService quizService;
        
        public Quiz Quiz { get; }

        public string Name { get; }

        public int QuestionCount { get; }

        
        public AsyncCommand RunQuizCommand { get; }

        public AsyncCommand EditQuizCommand { get; }

        public AsyncCommand DeleteQuizCommand { get; }

        
        public QuizCardViewModel(Quiz quiz, Services.Navigation.INavigationService navigationService, IQuizService quizService) 
        {
            Name = quiz.Name;
            QuestionCount = quiz.Questions.Count; 

            RunQuizCommand = new AsyncCommand(RunQuiz);
            EditQuizCommand = new AsyncCommand(EditQuiz);
            DeleteQuizCommand = new AsyncCommand(DeleteQuiz);

            this.Quiz = quiz;
            this.navigationService = navigationService;
            this.quizService = quizService;
        }

        private async Task RunQuiz()
        {
            await navigationService.NavigateTo<QuizQuestionViewModel>(new QuizRunNavigationInformation { QuestionToShow = Quiz.Questions.FirstOrDefault() });
        }

        private async Task EditQuiz()
        {
            await navigationService.NavigateTo<QuizEditViewModel>(new QuizEditNavigationInformation { QuizToEdit = Quiz }); 
        }

        private async Task DeleteQuiz()
        {
            await quizService.DeleteQuiz(Quiz);

            Messenger.Default.Send(new QuizDeletedMessage { DeletedQuiz = Quiz }); 
        }
    }
}
