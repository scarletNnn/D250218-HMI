using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;

namespace D250218.ViewModels;

public partial class IsEnableViewModel : ObservableObject
{
    private readonly IPermissionService _permissionService;

    public IsEnableViewModel(IModbusService modbusService, IPermissionService permissionService)
    {
        ModbusService = modbusService;
        _permissionService = permissionService;
        _permissionService.PermissionsChanged += OnPermissionsChanged;
        OnPermissionsChanged();
    }

    private void OnPermissionsChanged()
    {
        if (_permissionService.CurrentUser == null)
        {
            return;
        }
        IsEnable = _permissionService.CurrentUser.Level > 2;
    }

    public IsEnableViewModel() { }

    [ObservableProperty]
    public IModbusService _ModbusService;

    /// <summary>
    /// 是否启用
    /// </summary>
    [ObservableProperty]
    bool _isEnable;

    /// <summary>
    /// 使能切换
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    [RelayCommand]
    async Task BitChanged(object parameter)
    {
        try
        {
            int i = Convert.ToInt32(parameter);
            var res = ModbusService.DataBit[i];
            await ModbusService.WriteBitAsync((ushort)i, res);
        }
        catch (Exception ex)
        {
            ModbusService.Flag = true;
            MessageBox.Show(ex.Message);
        }
    }
}
