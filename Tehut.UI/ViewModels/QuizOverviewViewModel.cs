using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizOverviewViewModel : ViewModelBase, INavigationPage
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IActionBarService actionBarService;

        private const string navigationTitle = "Home"; 

        public ObservableCollection<QuizCardViewModel> Quizzes { get; } = new();

        public AsyncCommand AddQuizCommand { get; }

        public QuizOverviewViewModel(Services.Navigation.INavigationService navigationService, IActionBarService actionBarService)
        {
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 0, Name = "Egyptian Gods" }, navigationService)); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 1, Name = "Greek Gods" }, navigationService)); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 2, Name = "Roman Gods" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 3, Name = "U.S. Presidents" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 4, Name = "Basic Chemistry" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 5, Name = "German History" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 6, Name = "Swiss Mountains" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 7, Name = "Football" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 8, Name = "Coding" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 9, Name = "World Capitals" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 10, Name = "Animal Kingdom" }, navigationService));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 11, Name = "Architecture" }, navigationService));

            this.navigationService = navigationService;
            this.actionBarService = actionBarService;

            AddQuizCommand = new AsyncCommand(AddQuiz);
        }

        private async Task AddQuiz()
        {
            Quizzes.Add(new QuizCardViewModel(new Quiz { Name = "Added quiz" }, navigationService));
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            navigationService.SetNavigationTitle(navigationTitle); 

            actionBarService.SetActions(new List<IActionBarItem> 
            { 
                new ActionBarItem("Add Quiz", (viewModelBase) => Quizzes.Add(new QuizCardViewModel(new Quiz { Name = "Added quiz" }, navigationService)), ActionBarType.Add),
            });

            return Task.CompletedTask; 
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask;
        }
    }
}
