using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Messages;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels.Entities
{
    public class QuestionCardViewModel : ViewModelBase
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IQuizQuestionService quizQuestionService;

        public QuizQuestion Question { get; }
        
        public string QuestionText { get; set; }

        public string AnswerText1 { get; set; }

        public string AnswerText2 { get; set; }

        public string AnswerText3 { get; set; }

        public string AnswerText4 { get; set; }

        public List<string> Answers { get; set; }

        public AsyncCommand EditQuestionCommand { get; set; }

        public AsyncCommand DeleteQuestionCommand { get; set; }

        public QuestionCardViewModel(QuizQuestion question, Services.Navigation.INavigationService navigationService, IQuizQuestionService quizQuestionService) 
        {
            QuestionText = question.Question;
            AnswerText1 = question.Answer1; 
            AnswerText2 = question.Answer2; 
            AnswerText3 = question.Answer3; 
            AnswerText4 = question.Answer4; 

            EditQuestionCommand = new AsyncCommand(EditQuestion);
            DeleteQuestionCommand = new AsyncCommand(DeleteQuestion);

            this.Question = question;
            this.navigationService = navigationService;
            this.quizQuestionService = quizQuestionService;
        }

        private async Task EditQuestion()
        {
            await navigationService.NavigateTo<QuizQuestionEditViewModel>(new QuestionEditNavigationInformation { QuestionToEdit = Question });
        }

        private async Task DeleteQuestion()
        { 
            await quizQuestionService.DeleteQuestion(Question);

            Messenger.Default.Send(new QuestionDeletedMessage { DeletedQuestion = Question }); 
        }
    }
}
