using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        string NavigationTitle { get; }

        event EventHandler NavigationTitleChanged;

        Task NavigateTo<T>(NavigationInformation? navigationInformation = null, bool saveHistory = true) where T : ViewModelBase;

        Task NavigateToPreviousPage();

        Task NavigateToNextPage();

        bool CanNavigateToPreviousPage(); 

        bool CanNavigateToNextPage();

        void SetNavigationTitle(string title); 
    }
}
