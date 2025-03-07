using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace D250218.Converters;

public class WordToSpeedColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value.ToString())
        {
            case "0":
                return Brushes.CadetBlue;
            case "1":
                return Brushes.DarkSalmon;
            case "2":
                return Brushes.Crimson;
            default:
                return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
