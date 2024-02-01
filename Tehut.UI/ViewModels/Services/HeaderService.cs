using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services.Navigation; 

namespace Tehut.UI.ViewModels.Services
{
    public class HeaderService : ViewModelBase, IHeaderService
    {
        private readonly Navigation.INavigationService navigationService;

        public ObservableCollection<ActionBarItemViewModel> Actions { get; private set; } = new();

        private bool isSearchBarActive; 
        public bool IsSearchBarActive 
        { 
            get => isSearchBarActive;
            set 
            {
                isSearchBarActive = value; 
                RaisePropertyChanged(nameof(IsSearchBarActive));
            }
        }

        public HeaderService(Navigation.INavigationService navigationService) 
        {
            this.navigationService = navigationService;
        }

        public void SetActions(IEnumerable<IActionBarItem> actions)
        {
            Actions?.Clear();

            foreach(var action in actions) 
            {
                Actions?.Add(new ActionBarItemViewModel(navigationService, action)); 
            }
        }
    }
}
