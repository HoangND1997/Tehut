namespace Tehut.UI.ViewModels.Services.Navigation
{
    public interface INavigationPage
    {
        Task OnEnterPage(NavigationInformation navigationInformation); 

        Task OnExitPage();  
    }
}
