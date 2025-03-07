using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace D250218.Converters;

public class LevelIntToString : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is int level)
        {
            switch (level)
            {
                case 1:
                    return "Operator";
                case 2:
                    return "Engineer";
                case 3:
                    return "admin";
                default:
                    return "None";
            }
        }
        return "Unknown";
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
