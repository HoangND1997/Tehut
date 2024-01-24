using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly Services.INavigationService navigationService;

        public DelegateCommand QuizEditCommand { get; set; }
        public DelegateCommand QuizQuestionEditCommand { get; set; }
        public DelegateCommand QuizOverviewCommand { get; set; }
        public DelegateCommand QuizRunSummaryCommand { get; set; }
        public DelegateCommand QuizQuestionCommand { get; set; }

        public HeaderViewModel(Services.INavigationService navigationService) 
        {
            this.navigationService = navigationService;

            QuizEditCommand = new (NavigateToQuizEditView); 
            QuizOverviewCommand = new (NavigateToQuizOverviewView); 
            QuizQuestionEditCommand = new (NavigateToQuizQuestionEditView); 
            QuizRunSummaryCommand = new (NavigateToQuizRunSummaryView); 
            QuizQuestionCommand = new (NavigateToQuizQuestionView); 
        }

        private void NavigateToQuizEditView()
        {
            navigationService.NavigateTo<QuizEditViewModel>();
        }

        private void NavigateToQuizQuestionEditView()
        {
            navigationService.NavigateTo<QuizQuestionEditViewModel>();
        }

        private void NavigateToQuizRunSummaryView()
        {
            navigationService.NavigateTo<QuizRunSummaryViewModel>();
        }

        private void NavigateToQuizQuestionView()
        {
            navigationService.NavigateTo<QuizQuestionViewModel>();
        }

        private void NavigateToQuizOverviewView()
        {
            navigationService.NavigateTo<QuizOverviewViewModel>();
        }
    }
}