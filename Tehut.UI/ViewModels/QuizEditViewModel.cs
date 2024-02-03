using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Messages;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizEditViewModel : ViewModelBase, INavigationPage
    {
        private static readonly List<IActionBarItem> actions = new();

        private readonly Services.Navigation.INavigationService navigationService;

        private readonly IHeaderService headerService;
        private readonly IQuizService quizService;
        private readonly IQuizQuestionService quizQuestionService;

        public Quiz Quiz { get; private set; }

        public ObservableCollection<QuestionCardViewModel> Questions { get; private set; } = new(); 

        public AsyncCommand AddQuestionCommand { get; set; }

        public QuizEditViewModel(Services.Navigation.INavigationService navigationService, IHeaderService headerService, IQuizService quizService, IQuizQuestionService quizQuestionService)
        {
            this.navigationService = navigationService;
            this.headerService = headerService;
            this.quizService = quizService;
            this.quizQuestionService = quizQuestionService;

            actions.Add(new ActionBarItem("Add Question", async (viewModelBase) => await AddQuestion(), ActionBarType.Add));
            actions.Add(new ActionBarItem("Run Quiz", (viewModelBase) => { }, ActionBarType.Play));
            actions.Add(new ActionBarItem("Edit Quiz Name", (viewModelBase) => { }, ActionBarType.Edit));
            actions.Add(new ActionBarItem("Delete Quiz", async (viewModelBase) => await DeleteQuestion(), ActionBarType.Delete));

            AddQuestionCommand = new AsyncCommand(AddQuestion);

            Messenger.Default.Register<QuestionDeletedMessage>(this, OnQuestionDeleted);
        }

        #region Actions 

        private async Task AddQuestion()
        {
            var createdQuestion = await quizQuestionService.CreateQuestion(Quiz);

            Questions.Add(new QuestionCardViewModel(createdQuestion, navigationService, quizQuestionService));
        }

        private async Task DeleteQuestion()
        {
            await quizService.DeleteQuiz(Quiz);

            await navigationService.NavigateTo<QuizOverviewViewModel>();
        }

        #endregion

        private void OnQuestionDeleted(QuestionDeletedMessage questionDeletedMessage)
        {
            if (questionDeletedMessage?.DeletedQuestion is null)
            {
                return; 
            }

            var questionCardToBeRemoved = Questions.FirstOrDefault(q => q.Question.Id == questionDeletedMessage.DeletedQuestion.Id);

            if (questionCardToBeRemoved != null)
            {
                Questions.Remove(questionCardToBeRemoved); 
            }
        }

        private async Task LoadQuestions()
        {
            if (Quiz == null)
            {
                return; 
            }

            Questions.Clear();

            await quizService.LoadQuestionsFor(Quiz);

            foreach (var question in Quiz.Questions)
            {
                Questions.Add(new QuestionCardViewModel(question, navigationService, quizQuestionService)); 
            }
        }

        public async Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuizEditNavigationInformation quizInfo && quizInfo?.QuizToEdit is not null)
            {
                Quiz = quizInfo.QuizToEdit; 

                navigationService.SetNavigationTitle(quizInfo.QuizToEdit?.Name ?? string.Empty);

                await LoadQuestions(); 
            }

            headerService.SetActions(actions);
            headerService.IsSearchBarActive = true; 
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask; 
        }
    }
}
