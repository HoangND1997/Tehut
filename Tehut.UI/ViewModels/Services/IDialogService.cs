namespace Tehut.UI.ViewModels.Services
{
    public interface IDialogService
    {
        void ShowTextEditDialog(string title = "", string initialText = "", Func<string, Task> confirmCallback = null!, Func<Task> cancelCallback = null!);

        void ShowDeleteDialog(string title = "", string questionText = "", string warningText = "", Func<Task> deleteCallback = null!, Func<Task> cancelCallback = null!);
    }
}
