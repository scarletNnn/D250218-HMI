using CommunityToolkit.Mvvm.ComponentModel;
using SqliteDataAccess.Models;

namespace D250218.Services;

public partial class PermissionService : ObservableObject, IPermissionService
{
    private User _currentUser;
    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPermissionsChanged();
        }
    }
    public event Action PermissionsChanged;

    private void OnPermissionsChanged()
    {
        PermissionsChanged?.Invoke();
    }

    /// <summary>
    /// 获取当前用户
    /// </summary>
    /// <param name="user"></param>
    public void SetCurrentUser(User user)
    {
        CurrentUser = user;
    }

    /// <summary>
    /// 注销
    /// </summary>
    public void Logout()
    {
        if (CurrentUser == null)
            return;
        CurrentUser.Level = 0;
        CurrentUser.Username = "";
        OnPermissionsChanged();
    }
}
