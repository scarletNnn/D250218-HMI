using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace D250218.ViewModels;

public partial class ConnectSettingViewModel : ObservableValidator
{
    private readonly IModbusService _modbusService;

    public ConnectSettingViewModel(IModbusService modbusService)
    {
        _modbusService = modbusService;
        Test.Add(new() { Value = 0 });
        Test.Add(new() { Value = 0 });
        Test2.Add(0.0f);
    }

    public ConnectSettingViewModel() { }

    [ObservableProperty]
    ObservableCollection<DataInt> test = new();

    [ObservableProperty]
    string erroMessage;

    [ObservableProperty]
    ObservableCollection<float> test2 = new();

    [ObservableProperty]
    int value;

    [ObservableProperty]
    string _Ip = "127.0.0.1";

    partial void OnTestChanged(ObservableCollection<DataInt> value)
    {
        if (HasErrors)
        {
            ErroMessage = string.Join(Environment.NewLine, GetErrors());
        }
        ErroMessage = "";
    }

    [RelayCommand]
    async Task Connect()
    {
        try
        {
            await _modbusService.InitializeModbus(Ip);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    async Task Disconnect()
    {
        await _modbusService.DisconnectAsync();
    }

    [RelayCommand]
    void Add()
    {
        Test[0].Value = 123;
        //Test.Add(new() { Value = 431 });
        var s = Convert.ToDateTime("20250305");
    }
}

public partial class DataInt : ObservableValidator
{
    [ObservableProperty]
    [Range(0, 100)]
    int value;

    [ObservableProperty]
    int min;

    [ObservableProperty]
    int max;

    partial void OnValueChanged(int oldValue, int newValue)
    {
        ValidateProperty(newValue, nameof(Value));
        //if (HasErrors)
        //{
        //    Value = oldValue;
        //    //Value = Math.Clamp(Value, 0, 100);
        //}
    }
}
