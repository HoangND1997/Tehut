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

        public bool IsAnswerSelectable { get; set; } = true; 

        public bool ShowCorrectIndicator { get; set; }

        #endregion 

        public QuizRunViewModel(Services.Navigation.INavigationService navigationService, IHeaderService headerService) 
        {
            this.navigationService = navigationService;
            this.headerService = headerService;
        }

        public void SetAnswer(int userSelection)
        {
            runInformation?.Run?.UserAnswerPerQuestion.Add(runInformation.Run.CurrentQuestionIndex, userSelection);

            ShowAnswer(userSelection); 
        }

        private void ShowAnswer(int userSelection)
        {
            IsAnswerSelectable = false;
            ShowCorrectIndicator = true;

            RaisePropertyChanged(nameof(IsAnswerSelectable));
            RaisePropertyChanged(nameof(ShowCorrectIndicator));

            IsAnswer1Selected = userSelection == 0;
            IsAnswer2Selected = userSelection == 1;
            IsAnswer3Selected = userSelection == 2;
            IsAnswer4Selected = userSelection == 3;
        
            RaisePropertiesChanged(nameof(IsAnswer1Selected), nameof(IsAnswer2Selected), nameof(IsAnswer3Selected), nameof(IsAnswer4Selected));
        }

        private void ShowQuestion()
        {
            IsAnswerSelectable = true;
            ShowCorrectIndicator = false;

            RaisePropertyChanged(nameof(IsAnswerSelectable));
            RaisePropertyChanged(nameof(ShowCorrectIndicator));

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
                CurrentQuestion = Quiz?.Questions[quizRunInformation.Run.CurrentQuestionIndex];

                RaisePropertyChanged(nameof(QuestionText));
                RaisePropertyChanged(nameof(AnswerText1));
                RaisePropertyChanged(nameof(AnswerText2));
                RaisePropertyChanged(nameof(AnswerText3));
                RaisePropertyChanged(nameof(AnswerText4));
                RaisePropertyChanged(nameof(IsAnswer1Correct));
                RaisePropertyChanged(nameof(IsAnswer2Correct));
                RaisePropertyChanged(nameof(IsAnswer3Correct));
                RaisePropertyChanged(nameof(IsAnswer4Correct));

                if (quizRunInformation.Run.IsCurrentQuestionAnswered)
                {
                    ShowAnswer(quizRunInformation.Run.UserAnswerPerQuestion[quizRunInformation.Run.CurrentQuestionIndex]);
                }
                else 
                {
                    ShowQuestion(); 
                }
            }

            navigationService.SetNavigationTitle($"{Quiz?.Name}");

            headerService.SetActions(Enumerable.Empty<IActionBarItem>());

            headerService.IsSearchBarActive = false; 

            return Task.CompletedTask; 
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask;
        }
    }
}
