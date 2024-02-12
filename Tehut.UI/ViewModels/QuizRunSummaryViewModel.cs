using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizRunSummaryViewModel : ViewModelBase, INavigationPage
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IHeaderService headerService;

        public ObservableCollection<QuestionEntry> QuizRunEntries { get; } = new(); 

        public string AmountCorrectAnswers { get; set; }

        public string AmountIncorrectAnswers { get; set; }

        public string AmountSkippedAnswers { get; set; }

        public string AmountTotalQuestions { get; set; }    

        public AsyncCommand GoToHomeCommand { get; }  

        public QuizRunSummaryViewModel(Services.Navigation.INavigationService navigationService, IHeaderService headerService)
        {
            this.navigationService = navigationService;
            this.headerService = headerService;

            GoToHomeCommand = new(GoToHome);
        }

        private async Task GoToHome()
        {
            await navigationService.NavigateTo<QuizOverviewViewModel>(); 
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuizRunNavigationInformation runInformation && runInformation.Run is not null)
            {
                QuizRunEntries.Clear(); 

                AmountCorrectAnswers = $"{runInformation.Run.GetCorrectlyAnsweredQuestions().Count}";
                AmountIncorrectAnswers = $"{runInformation.Run.GetInCorrectlyAnsweredQuestions().Count}";
                AmountSkippedAnswers = $"{runInformation.Run.GetSkippedQuestions().Count}";
                AmountTotalQuestions = $"{runInformation.Run.Quiz?.Questions.Count ?? 0}";

                RaisePropertiesChanged(nameof(AmountCorrectAnswers), nameof(AmountIncorrectAnswers), nameof(AmountSkippedAnswers), nameof(AmountTotalQuestions));

                LoadEntries(runInformation.Run); 
            }

            headerService.SetActions(new List<IActionBarItem>());
            headerService.IsSearchBarActive = false; 

            return Task.CompletedTask;
        }

        private void LoadEntries(QuizRun run)
        {
            if (run.Quiz is null)
            {
                return; 
            }

            QuizRunEntries.Clear();

            foreach (var questionIndex in run.UserAnswerPerQuestion.Keys)
            {
                var state = run.UserAnswerPerQuestion[questionIndex] == -1 ? QuestionState.Unanswered 
                          : run.Quiz.Questions[questionIndex].CorrectAnswer == run.UserAnswerPerQuestion[questionIndex] ? QuestionState.Correct 
                          : QuestionState.Incorrect;
                
                QuizRunEntries.Add(new QuestionEntry(questionIndex + 1, state));
            }
        }   

        public Task OnExitPage<T>(T nextView) where T : ViewModelBase
        {
            return Task.CompletedTask;
        }
    }

    public record QuestionEntry(int QuestionIndex, QuestionState State); 
}
