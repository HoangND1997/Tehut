using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly Services.Navigation.INavigationService navigationService;

        public IActionBarService ActionBarService { get; }

        public AsyncCommand NavigateToPreviousPageCommand { get; set; }

        public AsyncCommand NavigateToNextPageCommand { get; set; }

        public AsyncCommand NavigateToHomeCommand { get; set; }

        public string HeaderTitle => navigationService?.NavigationTitle ?? string.Empty;  

        public HeaderViewModel(Services.Navigation.INavigationService navigationService, IActionBarService actionBarService) 
        {
            this.navigationService = navigationService;
            ActionBarService = actionBarService;

            navigationService.NavigationTitleChanged += (s, e) => RaisePropertyChanged(nameof(HeaderTitle)); 
            
            NavigateToPreviousPageCommand = new AsyncCommand(navigationService.NavigateToPreviousPage, navigationService.CanNavigateToPreviousPage);
            NavigateToNextPageCommand = new AsyncCommand(navigationService.NavigateToNextPage, navigationService.CanNavigateToNextPage);
            NavigateToHomeCommand = new AsyncCommand(async () => await navigationService.NavigateTo<QuizOverviewViewModel>());
        }
    }
}