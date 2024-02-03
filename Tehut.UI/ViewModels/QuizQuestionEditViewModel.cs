using DevExpress.Mvvm;
using Tehut.Core.Models;
using Tehut.Core.Services;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.ViewModels
{
    public class QuizQuestionEditViewModel : ViewModelBase, INavigationPage
    {
        private readonly IHeaderService headerService;
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IQuizQuestionService questionService;

        public QuizQuestion? Question { get; private set; }

        public string QuestionText  
        { 
            get => Question?.Question ?? string.Empty;
            set
            {
                if (Question == null)
                {
                    return; 
                }

                Question.Question = value;
                RaisePropertyChanged(nameof(QuestionText));
            }
        }

        public string AnswerText1
        {
            get => Question?.Answer1 ?? string.Empty;
            set
            {
                if (Question == null)
                {
                    return;
                }

                Question.Answer1 = value;
                RaisePropertyChanged(nameof(AnswerText1));
            }
        }

        public string AnswerText2
        {
            get => Question?.Answer2 ?? string.Empty;
            set
            {
                if (Question == null)
                {
                    return;
                }

                Question.Answer2 = value;
                RaisePropertyChanged(nameof(AnswerText2));
            }
        }

        public string AnswerText3
        {
            get => Question?.Answer3 ?? string.Empty;
            set
            {
                if (Question == null)
                {
                    return;
                }

                Question.Answer3 = value;
                RaisePropertyChanged(nameof(AnswerText3));
            }
        }

        public string AnswerText4
        {
            get => Question?.Answer4 ?? string.Empty;
            set
            {
                if (Question == null)
                {
                    return;
                }

                Question.Answer4 = value;
                RaisePropertyChanged(nameof(AnswerText4));
            }
        }

        public QuizQuestionEditViewModel(IHeaderService headerService, Services.Navigation.INavigationService navigationService, IQuizQuestionService questionService)
        {
            this.headerService = headerService;
            this.navigationService = navigationService;
            this.questionService = questionService;
        }

        public async Task SaveQuestion()
        {
            if (Question != null)
            {
                await questionService.SaveQuestion(Question); 
            }
        }

        public Task OnEnterPage(NavigationInformation navigationInformation)
        {
            if (navigationInformation is QuestionEditNavigationInformation questionEditInformation && questionEditInformation?.QuestionToEdit is not null)
            {
                Question = questionEditInformation.QuestionToEdit;

                navigationService.SetNavigationTitle(Question.Quiz?.Name ?? string.Empty);

                RaisePropertyChanged(nameof(QuestionText));
                RaisePropertyChanged(nameof(AnswerText1));
                RaisePropertyChanged(nameof(AnswerText2));
                RaisePropertyChanged(nameof(AnswerText3));
                RaisePropertyChanged(nameof(AnswerText4));
            }  

            headerService.SetActions(new List<IActionBarItem> 
            { 
                new ActionBarItem("Set Correct Answer", (viewModelBase) => { }, ActionBarType.SetCorrect), 
                new ActionBarItem("Delete Question", (viewModelBase) => { }, ActionBarType.Delete),
            });

            headerService.IsSearchBarActive = false; 

            return Task.CompletedTask;
        }

        public Task OnExitPage()
        {
            return Task.CompletedTask; 
        }
    }
}
