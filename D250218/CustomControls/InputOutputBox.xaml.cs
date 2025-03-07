using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace D250218.CustomControls;

/// <summary>
/// InputBox.xaml 的交互逻辑
/// </summary>
public partial class InputOutputBox : UserControl
{
    public InputOutputBox()
    {
        InitializeComponent();
    }

    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(InputOutputBox),
        new PropertyMetadata(null)
    );

    public object CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(InputOutputBox),
            new PropertyMetadata(0)
        );

    public bool Data
    {
        get { return (bool)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        "Data",
        typeof(bool),
        typeof(InputOutputBox),
        new FrameworkPropertyMetadata(
            false,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            DataChanged
        )
    );

    private static void DataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as InputOutputBox;
        if (control != null)
        {
            // 获取新的值
            bool newValue = (bool)e.NewValue;

            // 在这里添加你希望在 Data 属性更改时执行的逻辑
            // 例如，更新 UI 或触发其他操作

            //if (newValue)
            //{
            //    control.data.Background = Brushes.LightGreen;
            //}
            //else
            //{
            //    control.data.Background = Brushes.LightGray;
            //}
        }
    }

    /// <summary>
    /// 控件序号
    /// </summary>
    public string SerialNumber
    {
        get { return (string)GetValue(SerialNumberProperty); }
        set { SetValue(SerialNumberProperty, value); }
    }

    public static readonly DependencyProperty SerialNumberProperty = DependencyProperty.Register(
        "SerialNumber",
        typeof(string),
        typeof(InputOutputBox),
        new PropertyMetadata("")
    );

    /// <summary>
    /// 控件名称
    /// </summary>
    public string ControlName
    {
        get { return (string)GetValue(ControlNameProperty); }
        set { SetValue(ControlNameProperty, value); }
    }

    public static readonly DependencyProperty ControlNameProperty = DependencyProperty.Register(
        "ControlName",
        typeof(string),
        typeof(InputOutputBox),
        new PropertyMetadata("")
    );
}
