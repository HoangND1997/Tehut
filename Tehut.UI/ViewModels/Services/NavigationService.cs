using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services
{
    public class NavigationService : ViewModelBase, INavigationService
    {
        private readonly Func<Type, ViewModelBase> viewModelFactory;

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

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory) 
        {
            this.viewModelFactory = viewModelFactory;
        }

        public async Task NavigateTo<T>() where T : ViewModelBase
        {
            if (CurrentView is INavigationPage previousPage)
            { 
                await previousPage.OnExitPage();
            }

            var newView = viewModelFactory(typeof(T));

            if (newView is INavigationPage nextPage)
            {
                await nextPage.OnEnterPage(); 
            }

            CurrentView = newView;  
        }
    }
}
