using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizQuestionEditViewModel : ViewModelBase, INavigationPage
    {
        private readonly IActionBarService actionBarService;
        private readonly Services.Navigation.INavigationService navigationService;

        private string questionText; 
        public string QuestionText 
        { 
            get => questionText;
            set
            { 
                questionText = value;
                RaisePropertyChanged(nameof(QuestionText));
            }
        }

        private string answerText1;
        public string AnswerText1
        {
            get => answerText1;
            set
            {
                answerText1 = value;
                RaisePropertyChanged(nameof(AnswerText1));
            }
        }

        private string answerText2;
        public string AnswerText2
        {
            get => answerText2;
            set
            {
                answerText2 = value;
                RaisePropertyChanged(nameof(AnswerText2));
            }
        }

        private string answerText3;
        public string AnswerText3
        {
            get => answerText3;
            set
            {
                answerText3 = value;
                RaisePropertyChanged(nameof(AnswerText3));
            }
        }

        private string answerText4;
        public string AnswerText4
        {
            get => answerText4;
            set
            {
                answerText4 = value;
                RaisePropertyChanged(nameof(AnswerText4));
            }
        }

        public QuizQuestionEditViewModel(IActionBarService actionBarService, Services.Navigation.INavigationService navigationService)
        {
            this.actionBarService = actionBarService;
            this.navigationService = navigationService;
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuestionEditNavigationInformation questionEditInformation)
            {
                navigationService.SetNavigationTitle(questionEditInformation?.QuestionToEdit?.Quiz?.Name ?? string.Empty);

                var answers = questionEditInformation?.QuestionToEdit?.Answers ?? new List<string>();

                QuestionText = questionEditInformation?.QuestionToEdit?.Question ?? string.Empty; 

                AnswerText1 = answers.Count >= 1 ? answers[0] : string.Empty;
                AnswerText2 = answers.Count >= 2 ? answers[1] : string.Empty;
                AnswerText3 = answers.Count >= 3 ? answers[2] : string.Empty;
                AnswerText4 = answers.Count >= 4 ? answers[3] : string.Empty;
            }  

            actionBarService.SetActions(new List<IActionBarItem> { new ActionBarItem("Delete Quiz", (viewModelBase) => { }, ActionBarType.Delete) }); 

            return Task.CompletedTask;
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask; 
        }
    }
}
