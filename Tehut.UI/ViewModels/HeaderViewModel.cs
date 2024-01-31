using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        public IActionBarService ActionBarService { get; }

        public AsyncCommand NavigateToPreviousPageCommand { get; set; }

        public AsyncCommand NavigateToNextPageCommand { get; set; }

        public HeaderViewModel(Services.Navigation.INavigationService navigationService, IActionBarService actionBarService) 
        {
            ActionBarService = actionBarService;

            NavigateToPreviousPageCommand = new AsyncCommand(navigationService.NavigateToPreviousPage, navigationService.CanNavigateToPreviousPage);
            NavigateToNextPageCommand = new AsyncCommand(navigationService.NavigateToNextPage, navigationService.CanNavigateToNextPage); 
        }
    }
}