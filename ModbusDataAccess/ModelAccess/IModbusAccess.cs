namespace ModbusDataAccess.ModelAccess;

public interface IModbusAccess
{
    bool IsConnected { get; }
    Task ConnectAsync(string ip, int port);
    Task DisconnectAsync();
    Task<bool[]> ReadINAsync(ushort startAddress, ushort count);
    Task<bool[]> ReadOPAsync(ushort startAddress, ushort count);
    Task<bool[]> ReadBitAsync(ushort startAddress, ushort count);
    Task<short[]> ReadWordAsync(ushort startAddress, ushort count);
    Task<int[]> ReadLongAsync(ushort startAddress, ushort count);
    Task<float[]> ReadFloatAsync(ushort startAddress, ushort count);
    Task WriteOPAsync(ushort startAddress, bool value);
    Task WriteBitAsync(ushort startAddress, bool value);
    Task WriteWordAsync(ushort startAddress, ushort value);
    Task WriteLongAsync(ushort startAddress, int value);
    Task WriteFloatAsync(ushort startAddress, float value);
}
