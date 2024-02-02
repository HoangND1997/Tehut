using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuestionCardViewModel : ViewModelBase
    {
        private readonly QuizQuestion question;
        private readonly Services.Navigation.INavigationService navigationService;

        public string QuestionText { get; set; }

        public List<string> Answers { get; set; }

        public AsyncCommand EditQuestionCommand { get; set; }

        public AsyncCommand DeleteQuestionCommand { get; set; }

        public QuestionCardViewModel(QuizQuestion question, Services.Navigation.INavigationService navigationService) 
        {
            QuestionText = question.Question; 

            EditQuestionCommand = new AsyncCommand(EditQuestion);
            DeleteQuestionCommand = new AsyncCommand(DeleteQuestion);

            this.question = question;
            this.navigationService = navigationService;
        }

        private async Task EditQuestion()
        {
            await navigationService.NavigateTo<QuizQuestionEditViewModel>(new QuestionEditNavigationInformation { QuestionToEdit = question });
        }

        private async Task DeleteQuestion()
        { 
        
        }
    }
}
