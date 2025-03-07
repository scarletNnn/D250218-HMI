using System.Windows;
using D250218.ViewModels;
using D250218.Views;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace D250218.Services;

public class MyNavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;

    public MyNavigationService(
        IServiceProvider serviceProvider,
        INavigationService navigationService
    )
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
    }

    /// <summary>
    /// 导航到登入界面
    /// </summary>
    public void NavigateToLoginWindow()
    {
        var login = _serviceProvider.GetRequiredService<LoginWindow>();
        login.DataContext = _serviceProvider.GetRequiredService<LoginViewModel>();
        login.ShowDialog();
    }

    /// <summary>
    /// 关闭登入窗口
    /// </summary>
    /// <param name="window"></param>
    public void CloseLoginWindow(Window window)
    {
        window.Close();
    }

    /// <summary>
    /// 导航到首页界面
    /// </summary>
    public void NavigateToFirstView()
    {
        var vm = _serviceProvider.GetRequiredService<FirstViewModel>();
        _navigationService.Navigate(typeof(FirstView), vm);
    }

    /// <summary>
    /// 导航到输入1界面
    /// </summary>
    public void NavigateToInput1View()
    {
        var vm = _serviceProvider.GetRequiredService<Input1ViewModel>();
        _navigationService.Navigate(typeof(Input1View), vm);
    }

    /// <summary>
    /// 导航到输出1界面
    /// </summary>
    public void NavigateToOutput1View()
    {
        var vm = _serviceProvider.GetRequiredService<Output1ViewModel>();
        _navigationService.Navigate(typeof(Output1View), vm);
    }

    /// <summary>
    /// 导航到用户管理界面
    /// </summary>
    public void NavigateToUserManageView()
    {
        var vm = _serviceProvider.GetRequiredService<UserManageViewModel>();
        _navigationService.Navigate(typeof(UserManageView), vm);
    }

    /// <summary>
    /// 导航到参数界面
    /// </summary>
    public void NavigateToArgumentView()
    {
        var vm = _serviceProvider.GetRequiredService<ArgumentViewModel>();
        _navigationService.Navigate(typeof(ArgumentView), vm);
    }

    /// <summary>
    /// 导航到连接设置界面
    /// </summary>
    public void NavigateToConnectSettingView()
    {
        var vm = _serviceProvider.GetRequiredService<ConnectSettingViewModel>();
        _navigationService.Navigate(typeof(ConnectSettingView), vm);
    }

    /// <summary>
    /// 导航到使能界面
    /// </summary>
    public void NavigateToIsEnableView()
    {
        var vm = _serviceProvider.GetRequiredService<IsEnableViewModel>();
        _navigationService.Navigate(typeof(IsEnableView), vm);
    }

    /// <summary>
    /// 导航到报警信息界面
    /// </summary>
    public void NavigateToAlertView()
    {
        var vm = _serviceProvider.GetRequiredService<AlertViewModel>();
        _navigationService.Navigate(typeof(AlertView), vm);
    }

    /// <summary>
    /// 导航到示教1界面
    /// </summary>
    public void NavigateToWork1View()
    {
        var vm = _serviceProvider.GetRequiredService<Work1ViewModel>();
        _navigationService.Navigate(typeof(Work1View), vm);
    }
}
