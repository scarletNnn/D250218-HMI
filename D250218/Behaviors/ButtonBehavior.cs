using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace D250218.Behaviors;

class ButtonBehavior : Behavior<Button>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewMouseDown += OnPreviewMouseDown;
        AssociatedObject.PreviewMouseUp += OnPreviewMouseUp;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PreviewMouseDown -= OnPreviewMouseDown;
        AssociatedObject.PreviewMouseUp -= OnPreviewMouseUp;
    }

    private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (ReleaseCommand?.CanExecute(CommandParameter) == true)
            ReleaseCommand.Execute(CommandParameter);
    }

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (PressCommand?.CanExecute(CommandParameter) == true)
        {
            PressCommand.Execute(CommandParameter);
        }
        else
        {
            MessageBox.Show("权限不足！");
        }
    }

    /// <summary>
    /// 按下
    /// </summary>
    public ICommand PressCommand
    {
        get { return (ICommand)GetValue(PressCommandProperty); }
        set { SetValue(PressCommandProperty, value); }
    }

    public static readonly DependencyProperty PressCommandProperty = DependencyProperty.Register(
        "PressCommand",
        typeof(ICommand),
        typeof(ButtonBehavior)
    );

    /// <summary>
    /// 释放
    /// </summary>
    public ICommand ReleaseCommand
    {
        get { return (ICommand)GetValue(ReleaseCommandProperty); }
        set { SetValue(ReleaseCommandProperty, value); }
    }

    public static readonly DependencyProperty ReleaseCommandProperty = DependencyProperty.Register(
        "ReleaseCommand",
        typeof(ICommand),
        typeof(ButtonBehavior)
    );

    /// <summary>
    /// 参数
    /// </summary>
    public object CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonBehavior));
}
