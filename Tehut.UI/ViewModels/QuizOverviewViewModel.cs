using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Messages;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizOverviewViewModel : ViewModelBase, INavigationPage
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IHeaderService headerService;
        private readonly Services.IDialogService dialogService;
        private readonly IQuizService quizService;

        private ActionBarItem addQuizActionBarItem;

        private const string navigationTitle = "Home";
        private const string emptyQuizName = "New Quiz"; 

        public ObservableCollection<QuizCardViewModel> Quizzes { get; } = new();

        public AsyncCommand AddQuizCommand { get; }

        public QuizOverviewViewModel(Services.Navigation.INavigationService navigationService, IHeaderService headerService, Services.IDialogService dialogService, IQuizService quizService)
        {
            this.navigationService = navigationService;
            this.headerService = headerService;
            this.dialogService = dialogService;
            this.quizService = quizService;

            AddQuizCommand = new AsyncCommand(AddQuiz);

            addQuizActionBarItem = new ActionBarItem("Add Quiz", async (viewModelBase) => await AddQuiz(), ActionBarType.Add);

            Messenger.Default.Register<QuizDeletedMessage>(this, OnQuizDeleted);
        }

        #region Quiz Handling

        private async Task AddQuiz()
        {
            var createdQuiz = await quizService.CreateQuiz(emptyQuizName);    
            
            Quizzes.Add(new QuizCardViewModel(createdQuiz, navigationService, dialogService, quizService));
        }

        private async Task LoadQuizzes()
        {
            Quizzes.Clear();

            var quizzes = await quizService.GetAllQuizzes(); 

            foreach(var quiz in quizzes) 
            {
                Quizzes.Add(new QuizCardViewModel(quiz, navigationService, dialogService, quizService));
            }
        }

        private void OnQuizDeleted(QuizDeletedMessage quizDeletedMessage)
        {
            if (quizDeletedMessage?.DeletedQuiz == null)
            {
                return; 
            }

            var quizViewModelToBeRemoved = Quizzes.FirstOrDefault(q => q.Quiz.Id == quizDeletedMessage.DeletedQuiz.Id);

            if (quizViewModelToBeRemoved != null)
            {
                Quizzes.Remove(quizViewModelToBeRemoved);
            }
        }

        #endregion

        #region Page loading and closing

        public async Task OnEnterPage(NavigationInformation navigationInformation)
        {
            navigationService.SetNavigationTitle(navigationTitle);

            headerService.SetActions(new List<IActionBarItem> { addQuizActionBarItem });
            headerService.IsSearchBarActive = true;

            await LoadQuizzes();
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask;
        }

        #endregion 
    }
}
