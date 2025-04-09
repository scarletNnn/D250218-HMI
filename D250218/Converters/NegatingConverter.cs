using System.Windows.Data;

namespace D250218.Converters;

public class NegatingConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is double)
        {
            var res = -2 * (double)value;
            return res;
        }

        return value;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is double)
        {
            return +(double)value;
        }
        return value;
    }
}
