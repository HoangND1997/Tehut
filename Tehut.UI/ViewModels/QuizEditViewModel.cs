using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Entities;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizEditViewModel : ViewModelBase, INavigationPage
    {
        private static readonly List<IActionBarItem> actions = new();

        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IActionBarService actionBarService;

        public ObservableCollection<QuestionCardViewModel> Questions { get; private set; } = new(); 

        public QuizEditViewModel(Services.Navigation.INavigationService navigationService, IActionBarService actionBarService)
        {
            this.navigationService = navigationService;
            this.actionBarService = actionBarService;

            actions.Add(new ActionBarItem("Add Question", (viewModelBase) => { }, ActionBarType.Add));
            actions.Add(new ActionBarItem("Run Quiz", (viewModelBase) => { }, ActionBarType.Play));
            actions.Add(new ActionBarItem("Edit Quiz Name", (viewModelBase) => { }, ActionBarType.Edit));
            actions.Add(new ActionBarItem("Delete Quiz", (viewModelBase) => { }, ActionBarType.Delete));
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuizEditNavigationInformation quizInfo)
            { 
                navigationService.SetNavigationTitle(quizInfo.QuizToEdit?.Name ?? string.Empty);

                Questions.Clear();
                Questions.Add(new QuestionCardViewModel(new QuizQuestion
                {
                    Question = "What is the name of the egyptian god known as the god of moon and wisdom?",
                    Answers = new List<string>
                    {
                        "Anubis",
                        "Tehut",
                        "Osiris",
                        "Seth"
                    },
                    Quiz = quizInfo.QuizToEdit
                }, navigationService));
            }

            actionBarService.SetActions(actions);

            return Task.CompletedTask;
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask; 
        }
    }
}
