using System.Globalization;
using System.Windows.Data;

namespace D250218.Converters;

public class WordToSpeed : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value.ToString())
        {
            case "0":
                return "低速";
            case "1":
                return "中速";
            case "2":
                return "高速";
            default:
                return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
