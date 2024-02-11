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

        private readonly List<IActionBarItem> actions;

        private event EventHandler<int> CorrectAnswerChanged; 

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

        public bool IsAnswer1Correct
        {
            get => Question?.CorrectAnswer is 0;
            set 
            {
                if (IsAnswer1Correct && !value)
                {
                    return; 
                }

                CorrectAnswerChanged?.Invoke(this, 0); 
            }
        }

        public bool IsAnswer2Correct
        {
            get => Question?.CorrectAnswer is 1;
            set
            {
                if (IsAnswer2Correct && !value)
                {
                    return;
                }

                CorrectAnswerChanged?.Invoke(this, 1);
            }
        }

        public bool IsAnswer3Correct
        {
            get => Question?.CorrectAnswer is 2;
            set
            {
                if (IsAnswer3Correct && !value)
                {
                    return;
                }

                CorrectAnswerChanged?.Invoke(this, 2);
            }
        }

        public bool IsAnswer4Correct
        {
            get => Question?.CorrectAnswer is 3;
            set
            {
                if (IsAnswer4Correct && !value)
                {
                    return;
                }

                CorrectAnswerChanged?.Invoke(this, 3);
            }
        }

        public QuizQuestionEditViewModel(IHeaderService headerService, Services.Navigation.INavigationService navigationService, IQuizQuestionService questionService)
        {
            this.headerService = headerService;
            this.navigationService = navigationService;
            this.questionService = questionService;

            actions = new List<IActionBarItem>
            {
                new ActionBarItem("Delete Question", (viewModelBase) => DeleteQuestion(), ActionBarType.Delete),
            };

            CorrectAnswerChanged += OnCorrectAnswerChanged;
        }

        private async void OnCorrectAnswerChanged(object? sender, int correctAnswer)
        {
            if (Question == null)
            {
                return; 
            }

            Question.SetCorrectAnswer(correctAnswer);

            await questionService.SaveQuestion(Question); 

            RaisePropertyChanged(nameof(IsAnswer1Correct));
            RaisePropertyChanged(nameof(IsAnswer2Correct));
            RaisePropertyChanged(nameof(IsAnswer3Correct));
            RaisePropertyChanged(nameof(IsAnswer4Correct));
        }

        public async Task SaveQuestion()
        {
            if (Question != null)
            {
                await questionService.SaveQuestion(Question); 
            }
        }

        private async Task DeleteQuestion()
        { 
            if (Question != null) 
            { 
                await questionService.DeleteQuestion(Question);

                await navigationService.NavigateTo<QuizEditViewModel>(new QuizEditNavigationInformation { QuizToEdit = Question.Quiz });
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

            headerService.SetActions(actions);

            headerService.IsSearchBarActive = false; 

            return Task.CompletedTask;
        }

        public Task OnExitPage<T>(T nextView) where T : ViewModelBase
        {
            return Task.CompletedTask; 
        }
    }
}
