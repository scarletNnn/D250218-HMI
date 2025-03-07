using System.Globalization;
using System.Windows.Data;

namespace D250218.Converters;

public class BoolToStatus : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value == true)
            return "触发";
        return "解除";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
