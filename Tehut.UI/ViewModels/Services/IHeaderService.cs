using System.Collections.ObjectModel;
using Tehut.UI.ViewModels.Actions;

namespace Tehut.UI.ViewModels.Services
{
    public interface IHeaderService
    {
        ObservableCollection<ActionBarItemViewModel> Actions { get; }

        bool IsSearchBarActive { get; set; }

        void SetActions(IEnumerable<IActionBarItem> actions);
    }
}
