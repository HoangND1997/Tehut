using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    public class NavigationService : ViewModelBase, INavigationService
    {
        private ViewModelBase currentView = null!; 
        public ViewModelBase CurrentView
        {
            get => currentView; 
            set 
            {
                currentView = value; 
                RaisePropertyChanged(nameof(CurrentView));
            }
        }

        public string NavigationTitle { get; private set; } = string.Empty;

        public event EventHandler NavigationTitleChanged; 

        private readonly Func<Type, ViewModelBase> viewModelFactory;

        private List<(Type, NavigationInformation)> navigationHistory = [];

        private int navigationHistoryPosition = 0; 

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory) 
        {
            this.viewModelFactory = viewModelFactory;
        }

        public async Task NavigateTo<T>(NavigationInformation? navigationInformation = null) where T : ViewModelBase
        {
            if (CurrentView is T)
            {
                return;
            }

             await NavigateTo(typeof(T), navigationInformation, saveHistory: true);
        }

        public async Task NavigateToPreviousPage()
        {
            navigationHistoryPosition--;

            var (type, information) = navigationHistory[navigationHistoryPosition];

            await NavigateTo(type, information, saveHistory: false); 
        }

        public async Task NavigateToNextPage()
        {
            navigationHistoryPosition++;

            var (type, information) = navigationHistory[navigationHistoryPosition];

            await NavigateTo(type, information, saveHistory: false);
        }

        private async Task NavigateTo(Type viewType, NavigationInformation? navigationInformation = null, bool saveHistory = true)
        {
            if (CurrentView is INavigationPage previousPage)
            {
                await previousPage.OnExitPage();
            }

            var newView = viewModelFactory(viewType);

            if (newView is INavigationPage nextPage)
            {
                await nextPage.OnEnterPage(navigationInformation ?? NavigationInformation.Empty);

                if (saveHistory)
                {
                    navigationHistory = navigationHistoryPosition < navigationHistory.Count - 1 ? navigationHistory[..(navigationHistoryPosition + 1)] : navigationHistory; 
                    navigationHistory.Add((viewType, navigationInformation ?? NavigationInformation.Empty)); 

                    navigationHistoryPosition = navigationHistory.Count - 1;
                }
            }

            CurrentView = newView;
        }

        public bool CanNavigateToPreviousPage() => navigationHistoryPosition > 0 && navigationHistory.Count > 0;

        public bool CanNavigateToNextPage() => navigationHistoryPosition < navigationHistory.Count - 1;

        public void SetNavigationTitle(string title) 
        {
            if (NavigationTitle != title)
            {
                NavigationTitle = title; 
                NavigationTitleChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
