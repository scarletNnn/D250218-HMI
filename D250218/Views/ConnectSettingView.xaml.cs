using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using D250218.CustomControls;
using Wpf.Ui.Controls;

namespace D250218.Views;

/// <summary>
/// ConnectSettingView.xaml 的交互逻辑
/// </summary>
public partial class ConnectSettingView : Page
{
    Storyboard _storyboard;

    public ConnectSettingView()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        scrollControl.Content = scrollControl.Content + "qw";
        _storyboard?.Stop();
        double containerWidth = 600;
        double textWidth = scrollControl.ActualWidth;

        DoubleAnimation animation = new DoubleAnimation
        {
            From = containerWidth,
            To = 0.0 - textWidth,
            Duration = TimeSpan.FromSeconds((containerWidth + textWidth) / 50),
            RepeatBehavior = RepeatBehavior.Forever
        };

        Storyboard.SetTargetProperty(animation, new PropertyPath(TranslateTransform.XProperty));
        Storyboard.SetTarget(animation, textTransform);
        _storyboard = new Storyboard();
        _storyboard.Children.Add(animation);
        _storyboard.Begin();
    }
}
