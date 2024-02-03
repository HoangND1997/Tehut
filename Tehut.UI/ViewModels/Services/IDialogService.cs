namespace Tehut.UI.ViewModels.Services
{
    internal interface IDialogService
    {
        void ShowTextEditDialog(string initialText = "", Action<string> confirmAction = null!, Action cancelAction = null!);

        void ShowDeleteDialog(Action confirmAction = null!, Action cancelAction = null!);
    }
}
