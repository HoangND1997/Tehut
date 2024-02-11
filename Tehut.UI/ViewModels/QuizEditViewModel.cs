using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Messages;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;
using IDialogService = Tehut.UI.ViewModels.Services.IDialogService;

namespace Tehut.UI.ViewModels
{
    public class QuizEditViewModel : ViewModelBase, INavigationPage
    {
        private static readonly List<IActionBarItem> actions = new();

        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IHeaderService headerService;
        private readonly IQuizService quizService;
        private readonly IQuizQuestionService quizQuestionService;

        private const string defaultQuestionText = "[Question]";
        private const string defaultAnswerText1 = "[Option 1]";
        private const string defaultAnswerText2 = "[Option 2]";
        private const string defaultAnswerText3 = "[Option 3]";
        private const string defaultAnswerText4 = "[Option 4]";

        public Quiz Quiz { get; private set; }

        public ObservableCollection<QuestionCardViewModel> Questions { get; private set; } = new(); 

        public AsyncCommand AddQuestionCommand { get; set; }

        public QuizEditViewModel(Services.Navigation.INavigationService navigationService, 
                                IDialogService dialogService,
                                IHeaderService headerService, 
                                IQuizService quizService, 
                                IQuizQuestionService quizQuestionService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.headerService = headerService;
            this.quizService = quizService;
            this.quizQuestionService = quizQuestionService;

            actions.Add(new ActionBarItem("Add Question", async (viewModelBase) => await AddQuestion(), ActionBarType.Add));
            actions.Add(new ActionBarItem("Run Quiz", async (viewModelBase) => await RunQuiz(), ActionBarType.Play));
            actions.Add(new ActionBarItem("Edit Quiz Name", (viewModelBase) => EditQuiz(), ActionBarType.Edit));
            actions.Add(new ActionBarItem("Delete Quiz", async (viewModelBase) => await DeleteQuestion(), ActionBarType.Delete));

            AddQuestionCommand = new AsyncCommand(AddQuestion);

            Messenger.Default.Register<QuestionDeletedMessage>(this, OnQuestionDeleted);
        }

        #region Actions 

        private async Task RunQuiz()
        {
            await navigationService.NavigateTo<QuizRunViewModel>(new QuizRunNavigationInformation { Run = QuizRun.CreateFrom(Quiz) }); 
        }

        private async Task AddQuestion()
        {
            var createdQuestion = await quizQuestionService.CreateQuestion(Quiz);

            createdQuestion.Question = defaultQuestionText;
            createdQuestion.Answer1 = defaultAnswerText1;
            createdQuestion.Answer2 = defaultAnswerText2;
            createdQuestion.Answer3 = defaultAnswerText3;
            createdQuestion.Answer4 = defaultAnswerText4;

            await quizQuestionService.SaveQuestion(createdQuestion); 

            Questions.Add(new QuestionCardViewModel(createdQuestion, navigationService, quizQuestionService));

            await quizService.LoadQuestionsFor(Quiz);
        }

        private void EditQuiz()
        {
            dialogService.ShowTextEditDialog("Edit Quiz Name", Quiz.Name, async (newQuizName) => 
            { 
                Quiz.Name = newQuizName;

                await quizService.SaveQuiz(Quiz);

                navigationService.SetNavigationTitle(Quiz.Name);
            });
        }

        private async Task DeleteQuestion()
        {
            if (Quiz.Questions.Count == 0)
            {
                await quizService.DeleteQuiz(Quiz);

                await navigationService.NavigateTo<QuizOverviewViewModel>();

                return;
            }

            dialogService.ShowWarningDialog(StringTable.DeleteTitle, StringTable.DeleteQuestionText, StringTable.DeleteWarningText, StringTable.DeleteWarningButtonText, async () =>
            {
                await quizService.DeleteQuiz(Quiz);

                await navigationService.NavigateTo<QuizOverviewViewModel>();
            });
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
