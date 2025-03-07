using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace D250218.Converters;

public class BoolToRunType : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value == true)
            return "连续";
        return "寸动";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
