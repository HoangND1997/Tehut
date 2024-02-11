using System.Windows;

namespace Tehut.UI.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for DeleteDialog.xaml
    /// </summary>
    public partial class WarningDialog : Window
    {
        private readonly string title;
        private readonly string questionText;
        private readonly string warningText;
        private readonly string warningButtonText;
        private readonly Func<Task> confirmCallback;
        private readonly Func<Task> cancelCallback;

        public WarningDialog(string title = "", string questionText = "", string warningText = "", string warningButtonText = "", Func<Task> confirmCallback = null!, Func<Task> cancelCallback = null!)
        {
            InitializeComponent();

            Loaded += WarningDialog_Loaded;

            this.title = title;
            this.questionText = questionText;
            this.warningText = warningText;
            this.warningButtonText = warningButtonText;
            this.confirmCallback = confirmCallback;
            this.cancelCallback = cancelCallback;
        }

        private void WarningDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TitleBlock.Text = title;    

            QuestionTextBlock.Text = questionText; 
            WarningTextBlock.Text = warningText;    
            WarningButtonText.Content = warningButtonText;
        }

        private async Task Confirm()
        {
            if (confirmCallback is not null)
            { 
                await confirmCallback();
            }

            Close();
        }

        private async Task Cancel()
        {
            if (cancelCallback is not null)
            { 
                await cancelCallback();
            }

            Close(); 
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            await Confirm(); 
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            await Cancel();
        }

        private async void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            await Cancel();
        }
    }
}
