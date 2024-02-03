using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tehut.UI.Views.Components
{
    /// <summary>
    /// Interaction logic for EditableTextCard.xaml
    /// </summary>
    public partial class EditableTextCard : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EditableTextCard), new PropertyMetadata(""));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(HorizontalAlignment), typeof(EditableTextCard), new PropertyMetadata(HorizontalAlignment.Left));
        public static readonly DependencyProperty ShowNumberingProperty = DependencyProperty.Register("ShowNumbering", typeof(bool), typeof(EditableTextCard), new PropertyMetadata(false));
        public static readonly DependencyProperty NumberingTextProperty = DependencyProperty.Register("NumberingText", typeof(string), typeof(EditableTextCard), new PropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public HorizontalAlignment TextAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public bool ShowNumbering
        {
            get { return (bool)GetValue(ShowNumberingProperty); }
            set { SetValue(ShowNumberingProperty, value); }
        }

        public string NumberingText
        {
            get { return (string)GetValue(NumberingTextProperty); }
            set { SetValue(ShowNumberingProperty, value); }
        }

        public EditableTextCard()
        {
            InitializeComponent();
        }

        private void EditableTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseTextBox();
        }

        private void EditableTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Enter)
            {
                CloseTextBox();
            }
        }

        private void OpenTextBox()
        {
            EditableTextBox.IsReadOnly = false;

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                Keyboard.Focus(EditableTextBox);
                EditableTextBox.Focus();
                EditableTextBox.SelectAll();
            }));

            EditableTextBoxBorder.BorderBrush = FindResource("SecondaryTextColor") as SolidColorBrush;
            EditableTextBoxBorder.BorderThickness = new Thickness(2);
            EditableTextBoxBorder.Cursor = Cursors.Arrow;
        }

        private void CloseTextBox() 
        {
            EditableTextBox.IsReadOnly = true;

            Keyboard.ClearFocus();
            Focus();

            EditableTextBoxBorder.BorderBrush = FindResource("Base3Color") as SolidColorBrush;
            EditableTextBoxBorder.BorderThickness = new Thickness(1);
            EditableTextBoxBorder.Cursor = Cursors.Hand;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditableTextBox.IsReadOnly)
            {
                OpenTextBox();
            }
        }

        private void EditableTextBoxBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            { 
                OpenTextBox();
            }
        }

        private void EditableTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenTextBox();
        }
    }
}
