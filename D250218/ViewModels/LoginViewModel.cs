using System.Net;
using System.Security;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;
    private readonly MyNavigationService _myNavigationService;
    private readonly IPermissionService _permissionService;

    public LoginViewModel(
        IDataAccess dataAccess,
        User user,
        MyNavigationService myNavigationService,
        IPermissionService permissionService
    )
    {
        _dataAccess = dataAccess;
        User = user;
        _myNavigationService = myNavigationService;
        _permissionService = permissionService;
    }

    public LoginViewModel() { }

    /// <summary>
    /// 输入Username
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _username;

    /// <summary>
    /// 输入Password
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private SecureString _password;

    /// <summary>
    /// 错误信息
    /// </summary>
    [ObservableProperty]
    private string _errorMessage;

    [ObservableProperty]
    private User _User;

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="window"></param>
    [RelayCommand(CanExecute = nameof(CanExecuteLogin))]
    void Login(Window window)
    {
        var userLevel = _dataAccess.AuthenticateUser(new NetworkCredential(Username, Password));
        if (userLevel > 0)
        {
            User.Username = Username;
            User.Level = userLevel;
            _permissionService.SetCurrentUser(User);
            _myNavigationService.CloseLoginWindow(window);
        }
        else
        {
            User.Username = "";
            User.Level = 0;
            _permissionService.SetCurrentUser(User);
            ErrorMessage = "* Invalid username or password";
        }
    }

    /// <summary>
    /// 关闭登入窗口
    /// </summary>
    /// <param name="window"></param>
    [RelayCommand]
    void CloseLoginWindow(Window window)
    {
        _myNavigationService.CloseLoginWindow(window);
    }

    /// <summary>
    /// login按钮是否启用
    /// </summary>
    /// <returns></returns>
    bool CanExecuteLogin()
    {
        bool validData;
        if (string.IsNullOrWhiteSpace(Username) || Password == null || Password.Length < 1)
            validData = false;
        else
            validData = true;
        return validData;
    }
}
