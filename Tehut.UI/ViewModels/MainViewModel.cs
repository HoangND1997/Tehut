using DevExpress.Mvvm;
using Tehut.UI.ViewModels.Services;

namespace Tehut.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Services.Navigation.INavigationService Navigation { get; }

        public HeaderViewModel Header { get; }

        public MainViewModel(Services.Navigation.INavigationService navigation, HeaderViewModel header)
        {
            Navigation = navigation;
            Header = header;
        }
    }
}
