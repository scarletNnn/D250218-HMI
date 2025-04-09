using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using D250218.Models;
using ModbusDataAccess.ModelAccess;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.Services;

public partial class ModbusService : ObservableObject, IModbusService
{
    private readonly IModbusAccess _modbusAccess;
    private readonly IDataAccess _dataAccess;

    public ModbusService(IModbusAccess modbusAccess, IDataAccess dataAccess)
    {
        _modbusAccess = modbusAccess;
        _dataAccess = dataAccess;
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
    /// 产量Flag
    /// </summary>
    [ObservableProperty]
    bool _ProductionFlag;

    private CancellationTokenSource _cts;

    partial void OnProductionFlagChanged(bool value)
    {
        var msg = new ProductionMessage();
        msg.Value = value;
        msg.Timestamp = DateTime.Now;
        _dataAccess.InsertProductionMessage(msg);
    }

    /// <summary>
    /// 读写锁
    /// </summary>
    [ObservableProperty]
    bool _Flag = false;

    public async Task ConnectAsync(string ip, int port)
    {
        await _modbusAccess.ConnectAsync(ip, port);
    }

    public async Task DisconnectAsync()
    {
        _cts.Cancel();
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
            new() { Value = false, Message = "报警XXXXX1" },
            new() { Value = false, Message = "报警XXXXX2" },
            new() { Value = false, Message = "报警XXXXX3" },
            new() { Value = false, Message = "报警XXXXX4" },
            new() { Value = false, Message = "报警XXXXX5" },
            new() { Value = false, Message = "报警XXXXX6" },
            new() { Value = false, Message = "报警XXXXX7" }
        };
    }

    public async Task InitializeModbus(string ip)
    {
        await _modbusAccess.ConnectAsync(ip, 502);
        Flag = true;
        //Task task = new Task(async () =>
        //{
        //    await UpdateDataAsync();
        //});
        //task.Start();
        await Task.Run(async () =>
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
        _cts = new CancellationTokenSource();
        try
        {
            //if (!_modbusAccess.IsConnected)
            //    return;
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (Flag)
                    {
                        var IN = await _modbusAccess.ReadINAsync(0, 100);
                        var OP = await _modbusAccess.ReadOPAsync(0, 100);
                        var Bit = await _modbusAccess.ReadBitAsync(0, 100);
                        var Word = await _modbusAccess.ReadWordAsync(0, 100);
                        var Long = await _modbusAccess.ReadLongAsync(3000, 100);
                        var Float = await _modbusAccess.ReadFloatAsync(6000, 100);
                        var Alert = await _modbusAccess.ReadBitAsync(100, 7);
                        var Production = await _modbusAccess.ReadBitAsync(200, 1);

                        for (int i = 0; i < 100; i++)
                        {
                            DataIN[i] = IN[i];
                            DataOP[i] = OP[i];
                            DataBit[i] = Bit[i];
                            DataWord[i] = Word[i];
                            DataLong[i] = Long[i];
                            DataFloat[i] = Float[i];
                        }

                        for (int i = 0; i < 7; i++)
                        {
                            DataAlert[i].Value = Alert[i];
                        }

                        ProductionFlag = Production[0];
                    }
                    await Task.Delay(100, _cts.Token);
                }
                catch (OperationCanceledException)
                {
                    break; // 正常退出
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
