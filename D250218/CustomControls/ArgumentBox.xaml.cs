using System.Windows;
using System.Windows.Controls;

namespace D250218.CustomControls;

/// <summary>
/// ArgumentBox.xaml 的交互逻辑
/// </summary>
public partial class ArgumentBox : UserControl
{
    public ArgumentBox()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 数据名称
    /// </summary>
    public string DataName
    {
        get { return (string)GetValue(DataNameProperty); }
        set { SetValue(DataNameProperty, value); }
    }

    public static readonly DependencyProperty DataNameProperty = DependencyProperty.Register(
        "DataName",
        typeof(string),
        typeof(ArgumentBox),
        new PropertyMetadata("")
    );

    /// <summary>
    /// 数据
    /// </summary>
    public string Data
    {
        get { return (string)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        "Data",
        typeof(string),
        typeof(ArgumentBox),
        new PropertyMetadata("")
    );

    /// <summary>
    /// 数据单位
    /// </summary>
    public string DataUnit
    {
        get { return (string)GetValue(DataUnitProperty); }
        set { SetValue(DataUnitProperty, value); }
    }

    public static readonly DependencyProperty DataUnitProperty = DependencyProperty.Register(
        "DataUnit",
        typeof(string),
        typeof(ArgumentBox),
        new PropertyMetadata("")
    );

    public bool IsReadOnly
    {
        get { return (bool)GetValue(IsReadOnlyProperty); }
        set { SetValue(IsReadOnlyProperty, value); }
    }

    public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
        "IsReadOnly",
        typeof(bool),
        typeof(ArgumentBox),
        new PropertyMetadata(false)
    );

    public string Tag
    {
        get { return (string)GetValue(TagProperty); }
        set { SetValue(TagProperty, value); }
    }

    public static readonly DependencyProperty TagProperty = DependencyProperty.Register(
        "Tag",
        typeof(string),
        typeof(ArgumentBox),
        new PropertyMetadata("")
    );

    public int Min
    {
        get { return (int)GetValue(MinProperty); }
        set { SetValue(MinProperty, value); }
    }

    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
        "Min",
        typeof(int),
        typeof(ArgumentBox),
        new PropertyMetadata(0)
    );

    public int Max
    {
        get { return (int)GetValue(MaxProperty); }
        set { SetValue(MaxProperty, value); }
    }

    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
        "Max",
        typeof(int),
        typeof(ArgumentBox),
        new PropertyMetadata(0)
    );
}
