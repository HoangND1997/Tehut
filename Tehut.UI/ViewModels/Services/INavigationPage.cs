namespace Tehut.UI.ViewModels.Services
{
    public interface INavigationPage
    {
        Task OnEnterPage(); 

        Task OnExitPage();  
    }
}
