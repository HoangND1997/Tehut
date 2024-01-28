using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Services
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        Task NavigateTo<T>() where T : ViewModelBase; 
    }
}
