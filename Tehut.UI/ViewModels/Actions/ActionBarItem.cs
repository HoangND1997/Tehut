using DevExpress.Mvvm;

namespace Tehut.UI.ViewModels.Actions
{
    class ActionBarItem : IActionBarItem
    {
        private readonly Action<ViewModelBase> action;
        private readonly Func<ViewModelBase, Task> asyncAction; 

        public string Name { get; }

        public ActionBarType Type { get; }

        public ActionBarItem(string name, Func<ViewModelBase, Task> asyncAction, ActionBarType type = ActionBarType.None)
        {
            Name = name;
            Type = type; 

            this.asyncAction = asyncAction;
            this.action = null!; 
            
        }

        public ActionBarItem(string name, Action<ViewModelBase> action, ActionBarType type = ActionBarType.None)
        {
            Name = name;
            Type = type; 

            this.action = action;
            this.asyncAction = null!;
        }

        public async Task Execute(ViewModelBase currentContext)
        {
            if (action is not null)
            {
                action.Invoke(currentContext);
            
                return; 
            }

            await asyncAction.Invoke(currentContext);
        }
    }
}
