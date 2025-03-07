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
                return "待机";
            case "1":
                return "复位";
            case "2":
                return "就绪";
            case "3":
                return "自动运行";
            case "4":
                return "暂停";
            case "5":
                return "急停";
            default:
                return "待机";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
