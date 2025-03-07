using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace D250218.ValidationRules;

class ValueRange : ValidationRule
{
    public int? Min { get; set; }

    public int? Max { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        int currentValue = 0;

        try
        {
            currentValue = int.Parse((string)value);
        }
        catch (Exception e)
        {
            return new ValidationResult(false, e.Message);
        }

        if (Min is not null && currentValue < Min)
        {
            return new ValidationResult(false, $"小于最小值{Min}");
        }
        if (Max is not null && currentValue > Max)
        {
            return new ValidationResult(false, $"大于最大值{Max}");
        }

        return ValidationResult.ValidResult;
    }
}
