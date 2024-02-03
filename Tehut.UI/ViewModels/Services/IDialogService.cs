namespace Tehut.UI.ViewModels.Services
{
    public interface IDialogService
    {
        void ShowTextEditDialog(string title = "", string initialText = "", Func<string, Task> confirmCallback = null!, Func<Task> cancelCallback = null!);

        void ShowDeleteDialog(string title="", Func<Task> confirmCallback = null!, Func<Task> cancelCallback = null!);
    }
}
