using System.Globalization;
using System.Windows.Data;

namespace D250218.Converters;

public class StatusToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value.ToString())
        {
            case "0":
                return "Idle";
            case "1":
                return "Reset";
            case "2":
                return "Ready";
            case "3":
                return "Auto";
            case "4":
                return "Pause";
            case "5":
                return "Stop";
            default:
                return "Idle";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
