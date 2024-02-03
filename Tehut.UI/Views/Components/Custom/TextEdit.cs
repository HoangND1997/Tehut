using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tehut.UI.Views.Components.Custom
{
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Hint", Type = typeof(TextBlock))]
    public class TextEdit : TextBox
    {
        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set { SetValue(HintProperty, value); }
        }

        public SolidColorBrush HintColor
        {
            get { return (SolidColorBrush)GetValue(HintColorProperty); }
            set { SetValue(HintColorProperty, value); }
        }

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(TextEdit), new PropertyMetadata(""));
        public static readonly DependencyProperty HintColorProperty = DependencyProperty.Register("HintColor", typeof(SolidColorBrush), typeof(TextEdit), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        private TextBox? textBox;
        private TextBlock? hint; 

        static TextEdit()
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextEdit), new FrameworkPropertyMetadata(typeof(TextEdit)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GotFocus += TextEdit_GotFocus;
            LostFocus += TextEdit_LostFocus;

            textBox = Template.FindName("PART_TextBox", this) as TextBox;
            hint = Template.FindName("PART_Hint", this) as TextBlock;

            if (textBox != null)
            {
                textBox.GotFocus += TextEdit_GotFocus; 
                textBox.LostFocus += TextEdit_LostFocus;
            }
        }

        private void TextEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox?.Text) && hint != null)
            {
                hint.Visibility = Visibility.Visible;
            }
        }

        private void TextEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (hint != null)
            {
                hint.Visibility = Visibility.Collapsed;

                Keyboard.Focus(textBox);
            }
        }
    }
}
