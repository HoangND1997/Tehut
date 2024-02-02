using System.Globalization;
using System.Windows.Data;

namespace Tehut.UI.Views.Converters
{
    internal class QuestionCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int questionCount)
            {
                if (questionCount == 0)
                {
                    return "No Questions";
                }
                else if (questionCount == 1)
                {
                    return "1 Question";
                }
                else 
                {
                    return $"{questionCount} Questions";
                }
            }

            return string.Empty; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; 
        }
    }
}
