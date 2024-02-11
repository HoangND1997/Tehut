using Tehut.UI.Views;
using Tehut.UI.Views.Dialogs;

namespace Tehut.UI.ViewModels.Services
{
    public class DialogService : IDialogService
    {
        private readonly MainWindow owner;

        public DialogService(MainWindow owner)
        {
            this.owner = owner;
        }

        public void ShowWarningDialog(string title = "", string questionText = "", string warningText = "", string warningButtonText = "", Func<Task> confirmCallback = null!, Func<Task> cancelCallback = null!)
        {
            var deleteDialog = new WarningDialog(title, questionText, warningText, warningButtonText, confirmCallback, cancelCallback) { Owner = owner };  

            deleteDialog.ShowDialog();  
        }

        public void ShowTextEditDialog(string title = "", string initialText = "", Func<string, Task> confirmCallback = null!, Func<Task> cancelCallback = null!)
        {
            var textEditDialog = new TextEditDialog(title, initialText, confirmCallback, cancelCallback) { Owner = owner }; 

            textEditDialog.ShowDialog();
        }
    }
}
