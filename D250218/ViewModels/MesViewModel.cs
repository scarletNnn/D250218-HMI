using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;

namespace D250218.ViewModels;

public partial class MesViewModel : ObservableObject
{
    public static MesViewModel Instance { get; set; } = new();

    private readonly ISerialPortService _serialPortService;

    public MesViewModel(ISerialPortService serialPortService)
    {
        _serialPortService = serialPortService;
        // 初始化串口
        PortNames = _serialPortService.Init();
    }

    public MesViewModel() { }

    /// <summary>
    /// 选中的串口名称
    /// </summary>
    [ObservableProperty]
    private string _selectPortName;

    /// <summary>
    /// 可用的串口列表
    /// </summary>
    public List<string> PortNames { get; set; } = new List<string>();

    /// <summary>
    /// 连接状态
    /// </summary>
    [ObservableProperty]
    private bool _isConnected = false;

    /// <summary>
    /// 接收到的数据
    /// </summary>
    [ObservableProperty]
    private string _sn;

    /// <summary>
    /// 连接
    /// </summary>
    [RelayCommand]
    void Connect()
    {
        if (string.IsNullOrEmpty(SelectPortName))
        {
            MessageBox.Show("请选择串口");
            return;
        }
        string res = _serialPortService.Open(SelectPortName);
        MessageBox.Show(res);
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    [RelayCommand]
    void Disconnect()
    {
        _serialPortService.Close();
    }

    /// <summary>
    ///	扫码测试
    /// </summary>
    [RelayCommand]
    void ScanTest()
    {
        byte[] data = new byte[] { 0x4F, 0x4E };
        _serialPortService.Send(data, data.Length);
    }
}
