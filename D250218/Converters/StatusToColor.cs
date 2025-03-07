using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace D250218.Converters;

public class StatusToColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value.ToString())
        {
            case "0":
                return Brushes.AliceBlue;
            case "1":
                return Brushes.LightYellow;
            case "2":
                return Brushes.LightBlue;
            case "3":
                return Brushes.LightGreen;
            case "4":
                return Brushes.LightSalmon;
            case "5":
                return Brushes.Red;
            default:
                return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
