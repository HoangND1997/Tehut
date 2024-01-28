using System.Collections.ObjectModel;
using Tehut.UI.ViewModels.Actions;

namespace Tehut.UI.ViewModels.Services
{
    public interface IActionBarService
    {
        ObservableCollection<ActionBarItemViewModel> Actions { get; }

        void SetActions(IEnumerable<IActionBarItem> actions);
    }
}
