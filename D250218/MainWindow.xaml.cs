using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui;

namespace D250218;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    //{
    //    var grid = (Grid)sender;
    //    grid.Focusable = true; // 临时允许容器接收焦点
    //    grid.Focus(); // 强制焦点转移
    //    grid.Focusable = false; // 恢复容器的不可聚焦状态（可选）
    //}

    private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        FocusManager.SetFocusedElement(this, this);
    }
}
