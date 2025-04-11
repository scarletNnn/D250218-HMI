using System.Text;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;
using SqliteDataAccess.DbAccess;

namespace D250218.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly MyNavigationService _myNavigationService;
    private readonly IDataAccess _dataAccess;
    private readonly IPermissionService _permissionService;

    public MainViewModel(
        MyNavigationService myNavigationService,
        IDataAccess dataAccess,
        IPermissionService permissionService,
        IModbusService modbusService
    )
    {
        _myNavigationService = myNavigationService;
        _dataAccess = dataAccess;
        _permissionService = permissionService;
        ModbusService = modbusService;

        _permissionService.PermissionsChanged += () =>
        {
            Username = _permissionService.CurrentUser.Username;
            Level = _permissionService.CurrentUser.Level;
        };

        Init();

        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += Timer_Tick;
        timer.Start();

        ModbusService.DataAlert.ListChanged += DataAlert_ListChanged;
    }

    private void DataAlert_ListChanged(object? sender, System.ComponentModel.ListChangedEventArgs e)
    {
        StringBuilder alertText = new StringBuilder();
        for (int i = 0; i < ModbusService.DataAlert.Count; i++)
        {
            if (ModbusService.DataAlert[i].Value == true)
            {
                alertText.Append(ModbusService.DataAlert[i].Message);
                alertText.Append("   ");
            }
        }
        AlertText = alertText.ToString();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Date = DateTime.Now;
    }

    public MainViewModel() { }

    /// <summary>
    /// 当前Username
    /// </summary>
    [ObservableProperty]
    string _Username;

    /// <summary>
    /// 当前Level
    /// </summary>
    [ObservableProperty]
    int _Level;

    [ObservableProperty]
    public IModbusService _ModbusService;

    [ObservableProperty]
    string _AlertText;

    [ObservableProperty]
    DateTime _Date = DateTime.Now;

    /// <summary>
    /// 清除报警
    /// </summary>
    [RelayCommand]
    async Task AlertOff()
    {
        try
        {
            await ModbusService.WriteWordAsync(1, 100);
        }
        catch (Exception ex)
        {
            ModbusService.Flag = true;
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task Save()
    {
        try
        {
            await ModbusService.WriteWordAsync(1, 6);
        }
        catch (Exception ex)
        {
            ModbusService.Flag = true;
            MessageBox.Show(ex.Message);
        }
    }

    #region 导航
    /// <summary>
    /// 导航到登入界面
    /// </summary>
    [RelayCommand]
    void NavigateToLoginWindow()
    {
        _myNavigationService.NavigateToLoginWindow();
    }

    /// <summary>
    /// 注销
    /// </summary>
    [RelayCommand]
    void Logout()
    {
        _permissionService.Logout();
    }

    /// <summary>
    /// 导航到输入1界面
    /// </summary>
    [RelayCommand]
    void NavigateToInput1View()
    {
        _myNavigationService.NavigateToInput1View();
    }

    /// <summary>
    /// 导航到首页界面
    /// </summary>
    [RelayCommand]
    void NavigateToFirstView()
    {
        _myNavigationService.NavigateToFirstView();
    }

    /// <summary>
    /// 导航到输出1界面
    /// </summary>
    [RelayCommand]
    void NavigateToOutput1View()
    {
        _myNavigationService.NavigateToOutput1View();
    }

    /// <summary>
    /// 导航到用户管理界面
    /// </summary>
    [RelayCommand]
    void NavigateToUserManageView()
    {
        _myNavigationService.NavigateToUserManageView();
    }

    /// <summary>
    /// 导航到参数界面
    /// </summary>
    [RelayCommand]
    void NavigateToArgumentView()
    {
        _myNavigationService.NavigateToArgumentView();
    }

    /// <summary>
    /// 导航到连接设置界面
    /// </summary>
    [RelayCommand]
    void NavigateToConnectSettingView()
    {
        _myNavigationService.NavigateToConnectSettingView();
    }

    /// <summary>
    /// 导航到使能界面
    /// </summary>
    [RelayCommand]
    void NavigateToIsEnableView()
    {
        _myNavigationService.NavigateToIsEnableView();
    }

    /// <summary>
    /// 导航到报警信息界面
    /// </summary>
    [RelayCommand]
    void NavigateToAlertView()
    {
        _myNavigationService.NavigateToAlertView();
    }

    /// <summary>
    /// 导航到示教1界面
    /// </summary>
    [RelayCommand]
    void NavigatedToWork1View()
    {
        _myNavigationService.NavigateToWork1View();
    }

    /// <summary>
    /// 导航到Mes界面
    /// </summary>
    [RelayCommand]
    void NavigatedToMesView()
    {
        _myNavigationService.NavigateToMesView();
    }

    #endregion

    /// <summary>
    /// 初始化添加admin用户
    /// </summary>
    void Init()
    {
        _dataAccess.Init();
        var e = _dataAccess.UserExists("admin");
        if (!e.Result)
            _dataAccess.InsertUser("admin", "admin", 3);
    }
}
