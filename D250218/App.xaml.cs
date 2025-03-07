using System.Windows;
using D250218.Models;
using D250218.Services;
using D250218.ViewModels;
using D250218.Views;
using Microsoft.Extensions.DependencyInjection;
using ModbusDataAccess;
using SqliteDataAccess;
using Wpf.Ui;
using Wpf.Ui.Abstractions;

namespace D250218;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; }

    public static new App Current => (App)Application.Current;

    public App()
    {
        Services = ConfigureServices();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        // 注册窗口
        services.AddSingleton<MainWindow>(
            sp => new MainWindow { DataContext = sp.GetRequiredService<MainViewModel>() }
        );
        services.AddTransient<LoginWindow>();
        services.AddSingleton<FirstView>();
        services.AddSingleton<Input1View>();
        services.AddSingleton<Output1View>();
        services.AddSingleton<UserManageView>();
        services.AddSingleton<ArgumentView>();
        services.AddSingleton<ConnectSettingView>();
        services.AddSingleton<IsEnableView>();
        services.AddSingleton<AlertView>();
        services.AddSingleton<Work1View>();
        // 注册ViewModel
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<FirstViewModel>();
        services.AddSingleton<Input1ViewModel>();
        services.AddSingleton<Output1ViewModel>();
        services.AddSingleton<UserManageViewModel>();
        services.AddSingleton<ArgumentViewModel>();
        services.AddSingleton<ConnectSettingViewModel>();
        services.AddSingleton<IsEnableViewModel>();
        services.AddSingleton<AlertViewModel>();
        services.AddSingleton<Work1ViewModel>();
        // 注册服务
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<INavigationViewPageProvider, PageService>();
        services.AddSingleton<MyNavigationService>();
        services.AddSingleton<IPermissionService, PermissionService>();
        services.AddSingleton<IModbusService, ModbusService>();
        services.AddSqliteDataAccess();
        services.AddModbusDataAccess();

        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        //获取或设置一个值，此值指示数据绑定 TextBox 是否应显示与源的 Text 属性值一致的字符串。可以输入小数点。
        FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
        // 获取导航服务实例
        var navigationService = Services.GetRequiredService<INavigationService>();
        var mainWindow = Services.GetRequiredService<MainWindow>();
        // 设置 NavigationControl
        navigationService.SetNavigationControl(mainWindow.RootNavigation);

        mainWindow.Show();
    }
}
