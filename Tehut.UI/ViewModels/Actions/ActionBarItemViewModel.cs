using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Actions
{
    public class ActionBarItemViewModel : ViewModelBase
    {
        private readonly Services.Navigation.INavigationService navigationService;
        private readonly IActionBarItem actionBarItem;

        public string Name => actionBarItem.Name;

        public ActionBarType Type => actionBarItem.Type;

        public AsyncCommand ExecuteActionCommand { get; }

        public ActionBarItemViewModel(Services.Navigation.INavigationService navigationService, IActionBarItem actionBarItem)
        {
            this.navigationService = navigationService;
            this.actionBarItem = actionBarItem;

            ExecuteActionCommand = new AsyncCommand(ExecuteAction);
        }

        private async Task ExecuteAction()
        {
            await actionBarItem.Execute(navigationService.CurrentView); 
        }
    }
}
