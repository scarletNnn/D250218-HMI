using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;

namespace D250218.ViewModels;

public partial class Work1ViewModel : ObservableObject
{
    private readonly IPermissionService _permissionService;

    public Work1ViewModel(IModbusService modbusService, IPermissionService permissionService)
    {
        ModbusService = modbusService;
        _permissionService = permissionService;
        _permissionService.PermissionsChanged += OnPermissionChanged;
        OnPermissionChanged();
    }

    private void OnPermissionChanged()
    {
        if (_permissionService.CurrentUser == null)
        {
            Level = false;
            return;
        }
        Level = _permissionService.CurrentUser.Level > 2;
        IsReadOnly = !(_permissionService.CurrentUser.Level > 2);
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PressCommand))]
    [NotifyCanExecuteChangedFor(nameof(ReleaseCommand))]
    [NotifyCanExecuteChangedFor(nameof(TeachCommand))]
    [NotifyCanExecuteChangedFor(nameof(CalibrateCommand))]
    [NotifyCanExecuteChangedFor(nameof(RunTypeChangedCommand))]
    [NotifyCanExecuteChangedFor(nameof(SpeedTypeChangedCommand))]
    bool _Level;

    [ObservableProperty]
    public IModbusService _ModbusService;

    [ObservableProperty]
    bool _IsReadOnly = true;

    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="value"></param>
    [RelayCommand(CanExecute = (nameof(Level)))]
    void Press(object value)
    {
        try
        {
            ushort i = (ushort)Convert.ToInt16(value);
            ModbusService.WriteBitAsync(i, true);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 释放
    /// </summary>
    /// <param name="value"></param>
    [RelayCommand(CanExecute = (nameof(Level)))]
    void Release(object value)
    {
        try
        {
            ushort i = (ushort)Convert.ToInt16(value);
            ModbusService.WriteBitAsync(i, false);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 示教
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand(CanExecute = (nameof(Level)))]
    async Task Teach(string parameter)
    {
        try
        {
            ushort value = (ushort)Convert.ToInt16(parameter);
            await ModbusService.WriteWordAsync(10, value);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 校准
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand(CanExecute = (nameof(Level)))]
    async Task Calibrate(string parameter)
    {
        try
        {
            ushort value = (ushort)Convert.ToInt16(parameter);
            await ModbusService.WriteWordAsync(10, value);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 寸动/连续
    /// </summary>
    [RelayCommand(CanExecute = (nameof(Level)))]
    async Task RunTypeChangedAsync()
    {
        try
        {
            await ModbusService.WriteWordAsync(10, 6);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 速度类型
    /// </summary>
    [RelayCommand(CanExecute = (nameof(Level)))]
    async Task SpeedTypeChangedAsync()
    {
        try
        {
            await ModbusService.WriteBitAsync(10, true);
        }
        catch (Exception ex)
        {
            //ModbusService.Flag = true;
            ModbusService.Resume();
            MessageBox.Show(ex.Message);
        }
    }
}
