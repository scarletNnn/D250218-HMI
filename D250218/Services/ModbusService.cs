using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using D250218.Models;
using ModbusDataAccess.ModelAccess;
using SqliteDataAccess.Models;

namespace D250218.Services;

public partial class ModbusService : ObservableObject, IModbusService
{
    private readonly IModbusAccess _modbusAccess;

    public ModbusService(IModbusAccess modbusAccess)
    {
        _modbusAccess = modbusAccess;
        InitializeData();
    }

    [ObservableProperty]
    private ObservableCollection<bool> _DataIN = new();

    [ObservableProperty]
    private ObservableCollection<bool> _DataOP = new();

    [ObservableProperty]
    private ObservableCollection<bool> _DataBit = new();

    [ObservableProperty]
    private ObservableCollection<short> _DataWord = new();

    [ObservableProperty]
    private ObservableCollection<int> _DataLong = new();

    [ObservableProperty]
    private ObservableCollection<float> _DataFloat = new();

    [ObservableProperty]
    private BindingList<Alert> _DataAlert = new();

    /// <summary>
    /// 读写锁
    /// </summary>
    [ObservableProperty]
    bool _Flag = true;

    public async Task ConnectAsync(string ip, int port)
    {
        await _modbusAccess.ConnectAsync(ip, port);
    }

    public async Task DisconnectAsync()
    {
        await _modbusAccess.DisconnectAsync();
    }

    /// <summary>
    /// 初始化数据集合
    /// </summary>
    public void InitializeData()
    {
        for (ushort i = 0; i < 100; i++)
        {
            DataIN.Add(false);
            DataOP.Add(false);
            DataBit.Add(false);
            DataWord.Add(0);
            DataLong.Add(0);
            DataFloat.Add(0.0f);
        }
        DataAlert = new()
        {
            new() { Value = false, Message = "报警1" },
            new() { Value = false, Message = "报警2" },
            new() { Value = false, Message = "报警3" },
            new() { Value = false, Message = "报警4" },
            new() { Value = false, Message = "报警5" },
            new() { Value = false, Message = "报警6" }
        };
    }

    public async void InitializeModbus(string ip)
    {
        await _modbusAccess.ConnectAsync(ip, 502);
        Task task = new Task(async () =>
        {
            await UpdateDataAsync();
        });
        task.Start();
    }

    /// <summary>
    /// 读数据
    /// </summary>
    /// <returns></returns>
    private async Task UpdateDataAsync()
    {
        try
        {
            //if (!_modbusAccess.IsConnected)
            //    return;
            while (_modbusAccess.IsConnected)
            {
                if (Flag)
                {
                    var IN = await _modbusAccess.ReadINAsync(0, 100);
                    var OP = await _modbusAccess.ReadOPAsync(0, 100);
                    var Bit = await _modbusAccess.ReadBitAsync(0, 100);
                    var Word = await _modbusAccess.ReadWordAsync(0, 100);
                    var Long = await _modbusAccess.ReadLongAsync(3000, 100);
                    var Float = await _modbusAccess.ReadFloatAsync(6000, 100);
                    var Alert = await _modbusAccess.ReadBitAsync(100, 6);
                    for (int i = 0; i < 100; i++)
                    {
                        DataIN[i] = IN[i];
                        DataOP[i] = OP[i];
                        DataBit[i] = Bit[i];
                        DataWord[i] = Word[i];
                        DataLong[i] = Long[i];
                        DataFloat[i] = Float[i];
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        DataAlert[i].Value = Alert[i];
                    }

                    await Task.Delay(100);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public async Task WriteOPAsync(ushort startAddress, bool value)
    {
        Flag = false;
        await _modbusAccess.WriteOPAsync(startAddress, value);
        Flag = true;
    }

    public async Task WriteBitAsync(ushort startAddress, bool value)
    {
        Flag = false;
        await _modbusAccess.WriteBitAsync(startAddress, value);
        Flag = true;
    }

    public async Task WriteWordAsync(ushort startAddress, ushort value)
    {
        Flag = false;
        await _modbusAccess.WriteWordAsync(startAddress, value);
        Flag = true;
    }

    public async Task WriteLongAsync(ushort startAddress, int value)
    {
        Flag = false;
        await _modbusAccess.WriteLongAsync(startAddress, value);
        Flag = true;
    }

    public async Task WriteFloatAsync(ushort startAddress, float value)
    {
        Flag = false;
        await _modbusAccess.WriteFloatAsync(startAddress, value);
        Flag = true;
    }
}
