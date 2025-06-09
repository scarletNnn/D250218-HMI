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

public partial class ModbusService : ObservableObject, IModbusService, IDisposable
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
    /// 读写线程
    /// </summary>
    private Thread? _workerThread;

    /// <summary>
    /// 实现线程的暂停和继续
    /// </summary>
    private ManualResetEvent _isRunningEvent = new ManualResetEvent(false); // 初始是关闭的

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


    public async Task ConnectAsync(string ip, int port)
    {
        await _modbusAccess.ConnectAsync(ip, port);
    }

    public async Task DisconnectAsync()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            await _modbusAccess.DisconnectAsync();
        }
    }

    /// <summary>
    /// 初始化数据集合
    /// </summary>
    public void InitializeData()
    {
        for (ushort i = 0; i < 500; i++)
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
            new() { Value = false, Message = "报警XXXXX7" },
            new() { Value = false, Message = "报警XXXXX8" },
            new() { Value = false, Message = "报警XXXXX9" },
            new() { Value = false, Message = "报警XXXX10" }
        };
    }

    public async Task InitializeModbus(string ip)
    {
        await _modbusAccess.ConnectAsync(ip, 502);

        _workerThread = new Thread(UpdateDataAsync);
        _isRunningEvent.Set(); // 线程开始时放行
        _workerThread.Start();
    }

    /// <summary>
    /// 暂停线程
    /// </summary>
    public void Pause()
    {
        _isRunningEvent.Reset(); // 关闭信号量
    }

    /// <summary>
    /// 重置线程
    /// </summary>
    public void Resume()
    {
        _isRunningEvent.Set(); // 放行信号量
    }

    /// <summary>
    /// 读数据
    /// </summary>
    /// <returns></returns>
    private void UpdateDataAsync()
    {
        _cts = new CancellationTokenSource();
        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                _isRunningEvent.WaitOne(); // 等待信号量放行

                _cts.Token.ThrowIfCancellationRequested();

                var IN = _modbusAccess.ReadIN(0, 500);
                var OP = _modbusAccess.ReadOP(0, 500);
                var Bit = _modbusAccess.ReadBit(0, 500);
                var Word1 = _modbusAccess.ReadWord(0, 100);
                var Long1 = _modbusAccess.ReadLong(3000, 100);
                var Float1 = _modbusAccess.ReadFloat(6000, 100);
                var Word2 = _modbusAccess.ReadWord(100, 100);
                var Long2 = _modbusAccess.ReadLong(3100, 100);
                var Float2 = _modbusAccess.ReadFloat(6100, 100);

                var Alert = _modbusAccess.ReadBit(100, 10);
                var Production = _modbusAccess.ReadBit(200, 1);

                for (ushort i = 0; i < 500; i++)
                {
                    DataIN[i] = IN[i];
                    DataOP[i] = OP[i];
                    DataBit[i] = Bit[i];
                }

                for (ushort i = 0; i < 100; i++)
                {
                    DataWord[i] = Word1[i];
                    DataLong[i] = Long1[i];
                    DataFloat[i] = Float1[i];
                    DataWord[i + 100] = Word2[i];
                    DataLong[i + 100] = Long2[i];
                    DataFloat[i + 100] = Float2[i];
                }

                for (int i = 0; i < 10; i++)
                {
                    DataAlert[i].Value = Alert[i];
                }

                ProductionFlag = Production[0];

                Thread.Sleep(100);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public async Task WriteOPAsync(ushort startAddress, bool value)
    {
        Pause();
        await _modbusAccess.WriteOPAsync(startAddress, value);
        Resume();
    }

    public async Task WriteBitAsync(ushort startAddress, bool value)
    {
        Pause();
        await _modbusAccess.WriteBitAsync(startAddress, value);
        Resume();
    }

    public async Task WriteWordAsync(ushort startAddress, ushort value)
    {
        Pause();
        await _modbusAccess.WriteWordAsync(startAddress, value);
        Resume();
    }

    public async Task WriteLongAsync(ushort startAddress, int value)
    {
        Pause();
        await _modbusAccess.WriteLongAsync(startAddress, value);
        Resume();
    }

    public async Task WriteFloatAsync(ushort startAddress, float value)
    {
        Pause();
        await _modbusAccess.WriteFloatAsync(startAddress, value);
        Resume();
    }

    public void Dispose()
    {
        _modbusAccess.DisconnectAsync();
    }
}
