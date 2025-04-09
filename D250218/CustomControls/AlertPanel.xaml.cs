using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace D250218.CustomControls;

/// <summary>
/// AlertPanel.xaml 的交互逻辑
/// </summary>
public partial class AlertPanel : UserControl
{
    Storyboard _storyboard;

    public AlertPanel()
    {
        InitializeComponent();
        this.RenderTransform = new TranslateTransform();
    }

    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }

    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        "Message",
        typeof(string),
        typeof(AlertPanel),
        new PropertyMetadata("")
    );

    //private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    if (d is AlertPanel alertPanel)
    //    {
    //        alertPanel.Dispatcher.BeginInvoke(
    //            DispatcherPriority.Loaded,
    //            new Action(() =>
    //            {
    //                alertPanel._storyboard?.Stop();
    //                double containerWidth = 600;
    //                double textWidth = alertPanel.scrollControl.ActualWidth;

    //                DoubleAnimation animation = new DoubleAnimation
    //                {
    //                    From = containerWidth,
    //                    To = 0.0 - textWidth,
    //                    Duration = TimeSpan.FromSeconds((containerWidth + textWidth) / 50),
    //                    RepeatBehavior = RepeatBehavior.Forever
    //                };

    //                Storyboard.SetTargetProperty(
    //                    animation,
    //                    new PropertyPath(TranslateTransform.XProperty)
    //                );
    //                Storyboard.SetTarget(animation, alertPanel.textTransform);
    //                alertPanel._storyboard = new Storyboard();
    //                alertPanel._storyboard.Children.Add(animation);
    //                alertPanel._storyboard.Begin();
    //            })
    //        );
    //    }
    //}
}
