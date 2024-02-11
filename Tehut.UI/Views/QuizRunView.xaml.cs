using System.Windows.Controls;
using Tehut.UI.ViewModels;

namespace Tehut.UI.Views
{
    /// <summary>
    /// Interaction logic for QuizQuestionView.xaml
    /// </summary>
    public partial class QuizRunView : UserControl
    {
        public QuizRunView()
        {
            InitializeComponent();
        }

        private void QuestionTextCard1_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is QuizRunViewModel vm)
            {
                vm.SetAnswer(0);
            }
        }

        private void QuestionTextCard2_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is QuizRunViewModel vm)
            {
                vm.SetAnswer(1);
            }
        }

        private void QuestionTextCard3_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is QuizRunViewModel vm)
            {
                vm.SetAnswer(2);
            }
        }

        private void QuestionTextCard4_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is QuizRunViewModel vm)
            {
                vm.SetAnswer(3);
            }
        }
    }
}
