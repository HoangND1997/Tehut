using System.Windows;
using System.Windows.Input;

namespace Tehut.UI.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for TextEditDialog.xaml
    /// </summary>
    public partial class TextEditDialog : Window
    {
        private readonly string title;
        private readonly string initialValue;

        private readonly Func<string, Task> confirmAction;
        private readonly Func<Task> cancelAction;

        public TextEditDialog(string title = "Edit Quiz", string initialValue = "intial Value", Func<string, Task> confirmAction = null!, Func<Task> cancelAction = null!)
        {
            InitializeComponent();

            this.title = title;
            this.initialValue = initialValue;
            this.confirmAction = confirmAction;
            this.cancelAction = cancelAction;

            Loaded += TextEditDialog_Loaded;
        }

        private void TextEditDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TitleBlock.Text = title;

            if (!string.IsNullOrEmpty(initialValue))
            { 
                TextEdit.Text = initialValue;
            }

            TextEdit.Focus();
            TextEdit.SelectAll();

            Keyboard.Focus(TextEdit);
        }

        private async void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            await Cancel(); 
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            await Confirm(); 
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            await Cancel(); 
        }

        private async void TextEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Enter)
            { 
                await Confirm(); 
            }
        }

        private async Task Cancel()
        {
            if (cancelAction != null)
            {
                await cancelAction();
            }

            Close();
        }

        private async Task Confirm()
        {
            if (confirmAction != null)
            {
                await confirmAction(TextEdit.Text);
            }

            Close();
        }
    }
}
