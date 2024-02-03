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

        private readonly Action<string> confirmAction;
        private readonly Action cancelAction;

        public TextEditDialog(string title = "Edit Quiz", string initialValue = "intial Value", Action<string> confirmAction = null!, Action cancelAction = null!)
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            cancelAction?.Invoke();

            Close(); 
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            confirmAction?.Invoke(TextEdit.Text);

            Close(); 
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelAction?.Invoke(); 

            Close(); 
        }
    }
}
