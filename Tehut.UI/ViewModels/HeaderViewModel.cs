using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly Services.INavigationService navigationService;

        public IActionBarService ActionBarService { get; }

        public HeaderViewModel(Services.INavigationService navigationService, IActionBarService actionBarService) 
        {
            this.navigationService = navigationService;

            ActionBarService = actionBarService;
        }
    }
}