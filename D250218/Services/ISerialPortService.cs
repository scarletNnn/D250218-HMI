namespace D250218.Services;

public interface ISerialPortService : IDisposable
{
    List<string> Init();
    string Open(string portName);
    void Close();
    void Send(byte[] buffer, int count);
}
