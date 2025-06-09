using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;

namespace D250218.ViewModels;

public partial class Output1ViewModel : ObservableObject
{
    private readonly IPermissionService _permissionService;

    public Output1ViewModel(IPermissionService permissionService, IModbusService modbusService)
    {
        _permissionService = permissionService;
        ModbusService = modbusService;
        _permissionService.PermissionsChanged += OnPermissionsChanged;
        OnPermissionsChanged();
    }

    private void OnPermissionsChanged()
    {
        if (_permissionService.CurrentUser == null)
        {
            Level = 0;
            return;
        }
        Level = _permissionService.CurrentUser.Level;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OPChangedCommand))]
    int _Level;

    [ObservableProperty]
    public IModbusService _ModbusService;

    public Output1ViewModel() { }

    /// <summary>
    /// 权限等级判断
    /// </summary>
    /// <returns></returns>
    private bool Excute()
    {
        return Level > 0;
    }

    /// <summary>
    /// OP口开关
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(Excute))]
    async Task OPChanged(string parameter)
    {
        try
        {
            int i = Convert.ToInt32(parameter);
            var res = ModbusService.DataOP[i];
            await ModbusService.WriteOPAsync((ushort)i, !res);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }
}
