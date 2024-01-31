using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        Task NavigateTo<T>(NavigationInformation? navigationInformation = null) where T : ViewModelBase;

        Task NavigateToPreviousPage();

        Task NavigateToNextPage();

        bool CanNavigateToPreviousPage(); 

        bool CanNavigateToNextPage(); 
    }
}
