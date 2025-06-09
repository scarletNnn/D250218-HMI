namespace ModbusDataAccess.ModelAccess;

public interface IModbusAccess
{
    bool IsConnected { get; }
    Task ConnectAsync(string ip, int port);
    Task DisconnectAsync();
    bool[] ReadIN(ushort startAddress, ushort count);
    bool[] ReadOP(ushort startAddress, ushort count);
    bool[] ReadBit(ushort startAddress, ushort count);
    short[] ReadWord(ushort startAddress, ushort count);
    int[] ReadLong(ushort startAddress, ushort count);
    float[] ReadFloat(ushort startAddress, ushort count);
    Task WriteOPAsync(ushort startAddress, bool value);
    Task WriteBitAsync(ushort startAddress, bool value);
    Task WriteWordAsync(ushort startAddress, ushort value);
    Task WriteLongAsync(ushort startAddress, int value);
    Task WriteFloatAsync(ushort startAddress, float value);
}
