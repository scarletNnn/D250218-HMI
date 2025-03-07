using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using D250218.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xaml.Behaviors;

namespace D250218.Behaviors;

public class TextBoxBehavior : Behavior<TextBox>
{
    IModbusService _modbusService;

    protected override void OnAttached()
    {
        base.OnAttached();
        _modbusService = App.Current.Services.GetRequiredService<IModbusService>();
        AssociatedObject.GotFocus += AssociatedObject_GotFocus;
        AssociatedObject.LostFocus += AssociatedObject_LostFocusAsync;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
        AssociatedObject.LostFocus -= AssociatedObject_LostFocusAsync;
    }

    private void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        var textbox = sender as TextBox;
        if (textbox.IsReadOnly)
        {
            MessageBox.Show("权限不足");
            return;
        }
        _modbusService.Flag = false;
    }

    private async void AssociatedObject_LostFocusAsync(object sender, RoutedEventArgs e)
    {
        var textbox = sender as TextBox;
        var currentValue = textbox.Text;
        if (currentValue == null || currentValue == "")
            currentValue = "0";
        var index = Convert.ToInt32(textbox.Tag);
        try
        {
            if (index < 3000)
            {
                var value = (ushort)Convert.ToInt16(currentValue);
                await _modbusService.WriteWordAsync((ushort)index, value);
            }
            else if (3000 <= index && index < 3010)
            {
                var value = Convert.ToInt32(currentValue);
                await _modbusService.WriteLongAsync((ushort)index, value);
            }
            else if (3010 <= index && index < 6000)
            {
                var res = Convert.ToSingle(currentValue) * 1000;
                var value = Convert.ToInt32(res);
                await _modbusService.WriteLongAsync((ushort)index, value);
            }
            else if (6000 <= index)
            {
                var value = Convert.ToSingle(currentValue);
                await _modbusService.WriteFloatAsync((ushort)index, value);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            //如果 Command 或 Behavior 中涉及异步操作（如 async/await），但未正确等待异步任务，异常可能被隐藏在 Task 中未触发。
        }

        _modbusService.Flag = true;
    }
}
