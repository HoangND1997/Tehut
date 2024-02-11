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
    public partial class QuestionTextCard : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(QuestionTextCard), new PropertyMetadata(""));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof(TextAlignment), typeof(HorizontalAlignment), typeof(QuestionTextCard), new PropertyMetadata(HorizontalAlignment.Left));
        
        public static readonly DependencyProperty ShowNumberingProperty = DependencyProperty.Register(nameof(ShowNumbering), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));
        public static readonly DependencyProperty NumberingTextProperty = DependencyProperty.Register(nameof(NumberingText), typeof(string), typeof(QuestionTextCard), new PropertyMetadata(""));

        public static readonly DependencyProperty ShowCorrectIndicatorProperty = DependencyProperty.Register(nameof(ShowCorrectIndicator), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));
        public static readonly DependencyProperty IsCorrectProperty = DependencyProperty.Register(nameof(IsCorrect), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));
        
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(nameof(IsSelectable), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(QuestionTextCard), new PropertyMetadata(false));

        public static RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent(nameof(TextChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuestionTextCard));
        public static RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(nameof(Selected), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuestionTextCard));

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

        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        public event RoutedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        public bool ShowCorrectIndicator
        {
            get { return (bool)GetValue(ShowCorrectIndicatorProperty); }
            set { SetValue(ShowCorrectIndicatorProperty, value); }
        }

        public bool IsCorrect
        {
            get { return (bool)GetValue(IsCorrectProperty); }
            set { SetValue(IsCorrectProperty, value); }
        }

        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }   

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }   

        private string textOnOpen; 

        public QuestionTextCard()
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
            if (!IsEditable)
            {
                return;
            }

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

            textOnOpen = EditableTextBox.Text;
        }

        private void CloseTextBox() 
        {
            if (!IsEditable)
            {
                return;
            }

            EditableTextBox.IsReadOnly = true;

            Keyboard.ClearFocus();
            Focus();

            EditableTextBoxBorder.BorderBrush = FindResource("Base3Color") as SolidColorBrush;
            EditableTextBoxBorder.BorderThickness = new Thickness(1);
            EditableTextBoxBorder.Cursor = Cursors.Hand;

            if (EditableTextBox.Text != textOnOpen)
            { 
                RaiseEvent(new RoutedEventArgs(TextChangedEvent));
            }
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

            if (IsSelectable)
            { 
                RaiseEvent(new RoutedEventArgs(SelectedEvent)); 
            }
        }

        private void EditableTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenTextBox();
        }

        private void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            IsCorrect = !IsCorrect; 
        }
    }
}
