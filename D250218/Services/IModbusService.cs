using System.Collections.ObjectModel;
using System.ComponentModel;
using D250218.Models;
using SqliteDataAccess.Models;

namespace D250218.Services;

public interface IModbusService
{
    ObservableCollection<bool> DataIN { get; }
    ObservableCollection<bool> DataOP { get; }
    ObservableCollection<bool> DataBit { get; }
    ObservableCollection<short> DataWord { get; }
    ObservableCollection<int> DataLong { get; }
    ObservableCollection<float> DataFloat { get; }
    BindingList<Alert> DataAlert { get; }

    bool Flag { get; set; }
    Task ConnectAsync(string ip, int port);
    Task DisconnectAsync();
    void InitializeModbus(string ip);
    Task WriteOPAsync(ushort startAddress, bool value);
    Task WriteBitAsync(ushort startAddress, bool value);
    Task WriteWordAsync(ushort startAddress, ushort value);
    Task WriteLongAsync(ushort startAddress, int value);
    Task WriteFloatAsync(ushort startAddress, float value);
}
