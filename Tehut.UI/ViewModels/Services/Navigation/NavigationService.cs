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


        private CancellationTokenSource cancellationTokenSource = new();

        private readonly Func<Type, ViewModelBase> viewModelFactory;

        private List<(Type, NavigationInformation)> navigationHistory = new();

        private int navigationHistoryPosition = 0; 


        public NavigationService(Func<Type, ViewModelBase> viewModelFactory) 
        {
            this.viewModelFactory = viewModelFactory;
        }

        public async Task NavigateTo<T>(NavigationInformation? navigationInformation = null, bool saveHistory = true) where T : ViewModelBase
        {
            cancellationTokenSource = new(); 

            await NavigateTo(typeof(T), cancellationTokenSource.Token, navigationInformation, saveHistory: saveHistory);
        }

        public async Task NavigateToPreviousPage()
        {
            cancellationTokenSource = new();

            var (type, information) = navigationHistory[navigationHistoryPosition - 1];

            // only change the history position if the navigation was successful    
            if (await NavigateTo(type, cancellationTokenSource.Token, information, saveHistory: false))
            { 
                navigationHistoryPosition--;
            }
        }

        public async Task NavigateToNextPage()
        {
            cancellationTokenSource = new();

            var (type, information) = navigationHistory[navigationHistoryPosition + 1];

            // only change the history position if the navigation was successful    
            if (await NavigateTo(type, cancellationTokenSource.Token, information, saveHistory: false))
            { 
                navigationHistoryPosition++;
            }
        }

        private async Task<bool> NavigateTo(Type viewType, CancellationToken cancellationToken, NavigationInformation? navigationInformation = null, bool saveHistory = true)
        {
            var newView = viewModelFactory(viewType);

            if (CurrentView is INavigationPage previousPage)
            {
                await previousPage.OnExitPage(newView);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return false; 
            }

            if (newView is INavigationPage nextPage)
            {
                await nextPage.OnEnterPage(navigationInformation ?? NavigationInformation.Empty);
            }

            if (saveHistory)
            {
                SaveNavigationHistory(viewType, navigationInformation);
            }

            CurrentView = newView;

            return true; 
        }

        private void SaveNavigationHistory(Type viewType, NavigationInformation? navigationInformation)
        {
            navigationHistory = navigationHistoryPosition < navigationHistory.Count - 1 ? navigationHistory.Take(navigationHistoryPosition + 1).ToList() : navigationHistory;
            navigationHistory.Add((viewType, navigationInformation ?? NavigationInformation.Empty));

            navigationHistoryPosition = navigationHistory.Count - 1;
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

        public void RequestCancelNavigation()
        {
            cancellationTokenSource?.Cancel();  
        }
    }
}
