using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Actions
{
    public interface IActionBarItem
    {
        string Name { get; }

        ActionBarType Type { get; } 

        Task Execute(ViewModelBase currentContext); 
    }
}
