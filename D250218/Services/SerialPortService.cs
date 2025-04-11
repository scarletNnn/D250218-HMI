using System.IO.Ports;
using D250218.ViewModels;

namespace D250218.Services;

public class SerialPortService : ISerialPortService
{
    static SerialPort _serialPort;

    /// <summary>
    /// 扫描可用的串口
    /// </summary>
    /// <returns></returns>
    public List<string> Init()
    {
        return SerialPort.GetPortNames().ToList();
    }

    /// <summary>
    /// 打开串口
    /// </summary>
    /// <param name="potrName"></param>
    public string Open(string potrName)
    {
        _serialPort = new SerialPort();
        _serialPort.PortName = potrName;
        _serialPort.BaudRate = 9600;
        _serialPort.Parity = Parity.None;
        _serialPort.DataBits = 8;
        _serialPort.StopBits = StopBits.One;
        _serialPort.ReadTimeout = 1000;
        _serialPort.WriteTimeout = 1000;
        //_serialPort.ReceivedBytesThreshold = 10;
        //_serialPort.ReadBufferSize = 8;
        //_serialPort.Handshake = Handshake.XOnXOff;
        if (!_serialPort.IsOpen)
        {
            try
            {
                _serialPort.Open();
                _serialPort.DataReceived += (sender, e) =>
                {
                    // 处理接收到的数据
                    string data = _serialPort.ReadExisting();
                    // 这里可以根据需要处理数据，比如解析、显示等
                    MesViewModel.Instance.Sn = data;
                };

                return "串口打开成功";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        else
        {
            return "串口已打开";
        }
    }

    public void Send(byte[] buffer, int count)
    {
        try
        {
            _serialPort.Write(buffer, 0, count);
            _serialPort.DiscardInBuffer(); //清空接收缓冲区
            //_serialPort.WriteLine("42");
        }
        catch (Exception e)
        {
            ;
        }
    }

    /// <summary>
    /// 关闭串口
    /// </summary>
    public void Close()
    {
        if (_serialPort?.IsOpen == true)
        {
            _serialPort.Close();
        }
    }

    public void Dispose()
    {
        Close();
        _serialPort?.Dispose();
    }
}
