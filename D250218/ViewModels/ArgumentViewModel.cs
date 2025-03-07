using CommunityToolkit.Mvvm.ComponentModel;
using D250218.Services;

namespace D250218.ViewModels;

public partial class ArgumentViewModel : ObservableObject
{
    private readonly IPermissionService _permissionService;

    public ArgumentViewModel(IModbusService modbusService, IPermissionService permissionService)
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
        IsReadOnly = !(_permissionService.CurrentUser.Level > 1);
    }

    [ObservableProperty]
    public IModbusService _ModbusService;

    [ObservableProperty]
    bool _IsReadOnly = true;

    void Test() { }
}
