using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class QuizOverviewViewModel : ViewModelBase, INavigationPage
    {
        private readonly IActionBarService actionBarService;

        public ObservableCollection<QuizCardViewModel> Quizzes { get; } = new();

        public QuizOverviewViewModel(IActionBarService actionBarService)
        {
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 0, Name = "Egyptian Gods" })); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 1, Name = "Greek Gods" })); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 2, Name = "Roman Gods" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 3, Name = "U.S. Presidents" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 4, Name = "Basic Chemistry" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 5, Name = "German History" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 6, Name = "Swiss Mountains" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 7, Name = "Football" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 8, Name = "Coding" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 9, Name = "World Capitals" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 10, Name = "Animal Kingdom" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 11, Name = "Architecture" }));

            this.actionBarService = actionBarService;
        }

        public Task OnEnterPage()
        {
            actionBarService.SetActions(new List<IActionBarItem> 
            { 
                new ActionBarItem("Add Quiz", (viewModelBase) => Quizzes.Add(new QuizCardViewModel(new Quiz { Name = "Added quiz" })), ActionBarType.Add),
            });

            return Task.CompletedTask; 
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask;
        }
    }
}
