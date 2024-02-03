using System.Windows;
using System.Windows.Controls;
using Tehut.UI.ViewModels;

namespace Tehut.UI.Views
{
    /// <summary>
    /// Interaction logic for QuizQuestionEditView.xaml
    /// </summary>
    public partial class QuizQuestionEditView : UserControl
    {
        public QuizQuestionEditView()
        {
            InitializeComponent();
        }

        private async void EditableTextCard_TextChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuizQuestionEditViewModel vm)
            {
                await vm.SaveQuestion();
            }
        }
    }
}
