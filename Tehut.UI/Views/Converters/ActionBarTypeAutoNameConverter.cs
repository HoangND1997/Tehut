using System.Globalization;
using System.Windows.Data;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.Views.Automation;

namespace Tehut.UI.Views.Converters
{
    internal class ActionBarTypeAutoNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ActionBarType actionBarType)
            {
                return ActionBarAutoNames.ActionBarName(actionBarType);
            }

            return value; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
