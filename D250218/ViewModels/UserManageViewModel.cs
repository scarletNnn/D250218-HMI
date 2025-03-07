using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Enums;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class UserManageViewModel : ObservableObject
{
    public UserManageViewModel(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public UserManageViewModel() { }

    private readonly IDataAccess _dataAccess;

    [ObservableProperty]
    ObservableCollection<User> _users;

    [ObservableProperty]
    int _Id;

    [ObservableProperty]
    string _Username;

    [ObservableProperty]
    string _Password;

    [ObservableProperty]
    int _Level;

    [ObservableProperty]
    User selectedItems;

    [ObservableProperty]
    enumLevel _LevelEnum;

    /// <summary>
    /// 查询用户信息
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task SearchUser()
    {
        var list = await _dataAccess.GetAllUsers();
        Users = new ObservableCollection<User>(list);
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task AddUser()
    {
        await _dataAccess.InsertUser(Username, Password, Level);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task DeleteUser()
    {
        await _dataAccess.DeleteUser(Id);
    }

    /// <summary>
    /// 将选中的行信息展示到TextBox上
    /// </summary>
    /// <param name="value"></param>
    partial void OnSelectedItemsChanged(User value)
    {
        if (value != null)
        {
            Id = value.Id;
            Username = value.Username;
            Password = value.Password;
            Level = value.Level;
        }
    }
}
