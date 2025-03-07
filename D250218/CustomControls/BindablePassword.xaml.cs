using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace D250218.CustomControls;

/// <summary>
/// BindablePassword.xaml 的交互逻辑
/// </summary>
public partial class BindablePassword : UserControl
{
    public BindablePassword()
    {
        InitializeComponent();
        txtPassword.PasswordChanged += OnPasswordChanged;
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = txtPassword.SecurePassword; //获取当前由 PasswordBox 保存的密码，作为 SecureString。
    }

    public SecureString Password
    {
        get { return (SecureString)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
        "Password",
        typeof(SecureString),
        typeof(BindablePassword)
    );
}
