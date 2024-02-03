using System.Windows;

namespace Tehut.UI.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : Window
    {
        private readonly string title;
        private readonly string questionText;
        private readonly string warningText;

        private readonly Func<Task> deleteCallback;
        private readonly Func<Task> cancelCallback;

        public DeleteDialog(string title = "", string questionText = "", string warningText = "", Func<Task> deleteCallback = null!, Func<Task> cancelCallback = null!)
        {
            InitializeComponent();

            Loaded += DeleteDialog_Loaded;

            this.title = title;
            this.questionText = questionText;
            this.warningText = warningText;
            this.deleteCallback = deleteCallback;
            this.cancelCallback = cancelCallback;
        }

        private void DeleteDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TitleBlock.Text = title;    

            QuestionTextBlock.Text = questionText; 
            WarningTextBlock.Text = warningText;    
        }

        private async Task Delete()
        {
            if (deleteCallback is not null)
            { 
                await deleteCallback();
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

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await Delete(); 
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
