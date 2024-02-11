using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    public interface INavigationPage
    {
        Task OnEnterPage(NavigationInformation navigationInformation); 

        Task OnExitPage<T>(T nextView) where T : ViewModelBase;  
    }
}
