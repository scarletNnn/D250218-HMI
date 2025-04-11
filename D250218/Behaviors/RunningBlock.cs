using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace D250218.Behaviors;

[TemplatePart(Name = ElementContent, Type = typeof(FrameworkElement))]
[TemplatePart(Name = ElementPanel, Type = typeof(Plane))]
public class RunningBlock : ContentControl
{
    private const string ElementContent = "PART_ContentElement";

    private const string ElementPanel = "PART_Panel";

    protected Storyboard _storyboard;

    private FrameworkElement _elementContent;

    private FrameworkElement _elementPanel;

    public static readonly DependencyProperty RunningDirectionProperty =
        DependencyProperty.Register(
            nameof(RunningDirection),
            typeof(RunningDirection),
            typeof(RunningBlock),
            new PropertyMetadata(RunningDirection.EndToStart)
        );

    public RunningDirection RunningDirection
    {
        get => (RunningDirection)GetValue(RunningDirectionProperty);
        set => SetValue(RunningDirectionProperty, value);
    }
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        "Orientation",
        typeof(Orientation),
        typeof(RunningBlock),
        new FrameworkPropertyMetadata(
            Orientation.Horizontal,
            FrameworkPropertyMetadataOptions.AffectsRender
        )
    );

    //AffectsRender 更改此依赖属性的值会影响呈现或布局组合的某一方面（不是测量或排列过程） 当属性值改变时，会导致控件的重新渲染。

    public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
        "Duration",
        typeof(Duration),
        typeof(RunningBlock),
        new FrameworkPropertyMetadata(
            new Duration(TimeSpan.FromSeconds(5.0)),
            FrameworkPropertyMetadataOptions.AffectsRender
        )
    );

    public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register(
        "Speed",
        typeof(double),
        typeof(RunningBlock),
        new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender)
    );

    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public Duration Duration
    {
        get { return (Duration)GetValue(DurationProperty); }
        set { SetValue(DurationProperty, value); }
    }

    public double Speed
    {
        get { return (double)GetValue(SpeedProperty); }
        set { SetValue(SpeedProperty, value); }
    }

    public override void OnApplyTemplate()
    {
        if (_elementPanel != null)
        {
            _elementPanel.SizeChanged -= ElementPanel_SizeChanged;
        }

        base.OnApplyTemplate();
        _elementContent = GetTemplateChild("PART_ContentElement") as FrameworkElement; //返回实例化 ControlTemplate 的可视化树中的命名元素
        _elementPanel = GetTemplateChild("PART_Panel") as Panel;
        if (_elementPanel != null)
        {
            _elementPanel.SizeChanged += ElementPanel_SizeChanged;
        }

        UpdateContent();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        UpdateContent();
    }

    private void ElementPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateContent();
    }

    private void UpdateContent()
    {
        if (
            _elementContent == null
            || _elementPanel == null
            || _elementPanel.ActualWidth == 0
            || _elementPanel.ActualHeight == 0
        )
        {
            return;
        }

        _storyboard?.Stop();
        double numForm;
        double numTo;
        PropertyPath path;

        numTo = 0.0 - _elementPanel.ActualWidth;
        numForm = 600;

        path = new PropertyPath(
            "(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"
        );

        Duration duration = double.IsNaN(Speed)
            ? Duration
            : TimeSpan.FromSeconds(Math.Abs(numTo - numForm) / Speed);

        DoubleAnimation doubleAnimation = new DoubleAnimation(numForm, numTo, duration);

        doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        doubleAnimation.AutoReverse = false;
        Storyboard.SetTargetProperty(doubleAnimation, path);
        Storyboard.SetTarget(doubleAnimation, _elementContent);
        _storyboard = new Storyboard();
        _storyboard.Children.Add(doubleAnimation);
        _storyboard.Begin();
    }
}

public enum RunningDirection
{
    EndToStart,
    StartToEnd
}
