using System.Globalization;
using System.Windows.Data;

namespace D250218.Converters;

public class LongToDecimalPoint : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var currentValue = value.ToString();
        if (currentValue == null || currentValue == "")
            currentValue = "0";

        var res = System.Convert.ToInt32(currentValue);
        return res / 1000.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var currentValue = value.ToString();
        if (currentValue == null || currentValue == "")
            currentValue = "0";

        var res = System.Convert.ToSingle(currentValue) * 1000;

        return System.Convert.ToInt32(res);
    }
}
