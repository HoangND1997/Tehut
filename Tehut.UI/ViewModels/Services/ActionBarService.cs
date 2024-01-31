using System.Collections.ObjectModel;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.ViewModels.Services.Navigation; 

namespace Tehut.UI.ViewModels.Services
{
    public class ActionBarService : IActionBarService
    {
        private readonly INavigationService navigationService;

        public ObservableCollection<ActionBarItemViewModel> Actions { get; private set; } = new(); 

        public ActionBarService(INavigationService navigationService) 
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
