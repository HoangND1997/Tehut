namespace Tehut.UI.ViewModels.Services
{
    public interface IDialogService
    {
        void ShowTextEditDialog(string title = "", string initialText = "", Func<string, Task> confirmCallback = null!, Func<Task> cancelCallback = null!);

        void ShowWarningDialog(string title = "", string questionText = "", string warningText = "", string warningButtonText = "", Func<Task> confirmCallback = null!, Func<Task> cancelCallback = null!);
    }
}
