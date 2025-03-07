using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace D250218.Converters;

public class BoolToRunTypeColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value == true)
            return Brushes.MediumSpringGreen;
        return Brushes.DeepPink;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
