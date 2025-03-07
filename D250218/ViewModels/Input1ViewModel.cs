using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class Input1ViewModel : ObservableObject
{
    public Input1ViewModel(IModbusService modbusService)
    {
        ModbusService = modbusService;
    }

    public Input1ViewModel() { }

    [ObservableProperty]
    public IModbusService _ModbusService;
}
