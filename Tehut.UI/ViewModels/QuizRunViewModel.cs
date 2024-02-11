using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizRunViewModel : ViewModelBase, INavigationPage
    {
        private QuizRunNavigationInformation runInformation;
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IHeaderService headerService;
        private readonly Services.IDialogService dialogService;
        private List<IActionBarItem> actions = new();

        #region Properties 

        public Quiz? Quiz { get; private set; }

        public QuizQuestion? CurrentQuestion { get; private set; }

        public string QuestionText => CurrentQuestion?.Question ?? "";

        public string AnswerText1 => CurrentQuestion?.Answer1 ?? "";

        public string AnswerText2 => CurrentQuestion?.Answer2 ?? "";

        public string AnswerText3 => CurrentQuestion?.Answer3 ?? "";

        public string AnswerText4 => CurrentQuestion?.Answer4 ?? "";

        public bool IsAnswer1Correct => CurrentQuestion?.CorrectAnswer is 0;

        public bool IsAnswer2Correct => CurrentQuestion?.CorrectAnswer is 1;

        public bool IsAnswer3Correct => CurrentQuestion?.CorrectAnswer is 2;

        public bool IsAnswer4Correct => CurrentQuestion?.CorrectAnswer is 3;

        public bool IsAnswer1Selected { get; set; }

        public bool IsAnswer2Selected { get; set; }

        public bool IsAnswer3Selected { get; set; }

        public bool IsAnswer4Selected { get; set; }
        public bool IsQuestionAnswered { get; set; }

        public AsyncCommand NextQuestionCommand { get; set; }

        #endregion 

        public QuizRunViewModel(Services.Navigation.INavigationService navigationService, IHeaderService headerService, Services.IDialogService dialogService) 
        {
            this.navigationService = navigationService;
            this.headerService = headerService;
            this.dialogService = dialogService;

            NextQuestionCommand = new AsyncCommand(ToNextQuestion);

            actions = new List<IActionBarItem>
            {
                new ActionBarItem("Reveal Answer", (viewModelBase) => RevealAnswer(), ActionBarType.Reveal),
                new ActionBarItem("Leave", (viewModelBase) => LeaveQuiz(), ActionBarType.Exit)
            };
        }

        public void SetAnswer(int userSelection)
        {
            runInformation?.Run?.UserAnswerPerQuestion.Add(runInformation.CurrentQuestionIndex, userSelection);

            ShowAnswer(userSelection); 
        }

        #region Actions 

        private void RevealAnswer()
        {
            SetAnswer(-1);
        }

        private void LeaveQuiz()
        {
            dialogService.ShowWarningDialog(StringTable.LeaveTitle, StringTable.LeaveQuestionText, StringTable.LeaveWarningText, StringTable.LeaveWarningButtonText, async () =>
            {
                await navigationService.NavigateTo<QuizOverviewViewModel>();
            });
        }

        #endregion 


        private async Task ToNextQuestion()
        {
            if (runInformation.Run is null)
            {
                return; 
            }

            await navigationService.NavigateTo<QuizRunViewModel>(runInformation.GetNextQuestion());
        }

        private void ShowAnswer(int userSelection)
        {
            IsQuestionAnswered = true; 

            RaisePropertyChanged(nameof(IsQuestionAnswered));

            IsAnswer1Selected = userSelection == 0;
            IsAnswer2Selected = userSelection == 1;
            IsAnswer3Selected = userSelection == 2;
            IsAnswer4Selected = userSelection == 3;
        
            RaisePropertiesChanged(nameof(IsAnswer1Selected), nameof(IsAnswer2Selected), nameof(IsAnswer3Selected), nameof(IsAnswer4Selected));
        }

        private void ShowQuestion()
        {
            IsQuestionAnswered = false; 

            RaisePropertyChanged(nameof(IsQuestionAnswered));

            IsAnswer1Selected = false;
            IsAnswer2Selected = false;
            IsAnswer3Selected = false;
            IsAnswer4Selected = false;

            RaisePropertiesChanged(nameof(IsAnswer1Selected), nameof(IsAnswer2Selected), nameof(IsAnswer3Selected), nameof(IsAnswer4Selected));
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuizRunNavigationInformation quizRunInformation && quizRunInformation?.Run is not null)
            { 
                this.runInformation = quizRunInformation;

                Quiz = quizRunInformation.Run.Quiz; 
                CurrentQuestion = Quiz?.Questions[quizRunInformation.CurrentQuestionIndex];

                RaisePropertyChanged(nameof(QuestionText));

                RaisePropertyChanged(nameof(AnswerText1));
                RaisePropertyChanged(nameof(AnswerText2));
                RaisePropertyChanged(nameof(AnswerText3));
                RaisePropertyChanged(nameof(AnswerText4));

                RaisePropertyChanged(nameof(IsAnswer1Correct));
                RaisePropertyChanged(nameof(IsAnswer2Correct));
                RaisePropertyChanged(nameof(IsAnswer3Correct));
                RaisePropertyChanged(nameof(IsAnswer4Correct));

                if (quizRunInformation.Run.IsCurrentQuestionAnswered(runInformation.CurrentQuestionIndex))
                {
                    ShowAnswer(quizRunInformation.Run.UserAnswerPerQuestion[quizRunInformation.CurrentQuestionIndex]);
                }
                else 
                {
                    ShowQuestion(); 
                }

                navigationService.SetNavigationTitle($"{Quiz?.Name} ({quizRunInformation.CurrentQuestionIndex + 1}/{Quiz?.Questions.Count})");
            }

            headerService.SetActions(actions);

            headerService.IsSearchBarActive = false; 

            return Task.CompletedTask; 
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask;
        }
    }
}
