﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace D250218.CustomControls;

/// <summary>
/// AlertPanel.xaml 的交互逻辑
/// </summary>
public partial class AlertPanel : UserControl
{
    private Storyboard _currentStoryboard;

    public AlertPanel()
    {
        InitializeComponent();
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
}
