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

        public void NavigateTo<T>() where T : ViewModelBase
        {
            CurrentView = viewModelFactory(typeof(T));    
        }
    }
}
