using System.Collections;
using System.Net.Sockets;
using System.Windows;
using NModbus;
using NModbus.Device;

namespace ModbusDataAccess.ModelAccess;

public class ModbusAccess : IModbusAccess
{
    private TcpClient _tcpClient;
    private IModbusMaster _modbusMaster;
    public bool IsConnected => _tcpClient?.Connected ?? false;

    public async Task ConnectAsync(string ip, int port)
    {
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync(ip, port);
        var factory = new ModbusFactory();
        _modbusMaster = factory.CreateMaster(_tcpClient);
    }

    public async Task DisconnectAsync()
    {
        _modbusMaster?.Dispose();
        _modbusMaster = null;
        _tcpClient?.Close();
        await Task.CompletedTask;
    }

    #region Read
    /// <summary>
    /// 读IN
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool[] ReadIN(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadInputs(1, startAddress, count);
        return result;
    }

    /// <summary>
    /// 读OP
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool[] ReadOP(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadInputs(1, (ushort)(startAddress + 10000), count);
        return result;
    }

    /// <summary>
    /// 读Bit
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool[] ReadBit(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadCoils(1, startAddress, count);
        return result;
    }

    /// <summary>
    /// 读Word
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public short[] ReadWord(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadHoldingRegisters(1, startAddress, count);
        short[] res = new short[count];
        for (int i = 0; i < result.Length; i++)
        {
            res[i] = (short)result[i];
        }

        return res;
    }

    /// <summary>
    /// 读Long
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int[] ReadLong(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadHoldingRegisters(1, startAddress, count);
        int[] res = new int[count];
        for (int i = 0; i < result.Length / 2; i++)
        {
            int value = result[i * 2] + (result[i * 2 + 1] << 16);
            res[i] = value;
        }
        return res;
    }

    /// <summary>
    /// 读Float
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public float[] ReadFloat(ushort startAddress, ushort count)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException();
        var result = _modbusMaster.ReadHoldingRegisters(1, startAddress, count);
        byte[] byteArray = result.SelectMany(BitConverter.GetBytes).ToArray();
        float value = BitConverter.ToSingle(byteArray, 0); //ABCD=>BADC
        float[] res = new float[count];
        for (int i = 0; i < result.Length / 2; i++)
        {
            res[i] = BitConverter.ToSingle(byteArray, i * 4);
        }
        return res;
    }
    #endregion

    #region Write
    /// <summary>
    /// 写OP
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task WriteOPAsync(ushort startAddress, bool value)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException("网络未连接!");
        await _modbusMaster.WriteSingleCoilAsync(1, (ushort)(20000 + startAddress), value);
    }

    public async Task WriteBitAsync(ushort startAddress, bool value)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException("网络未连接!");
        await _modbusMaster.WriteSingleCoilAsync(1, startAddress, value);
    }

    public async Task WriteWordAsync(ushort startAddress, ushort value)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException("网络未连接!");
        await _modbusMaster.WriteSingleRegisterAsync(1, startAddress, value);
    }

    public async Task WriteLongAsync(ushort startAddress, int value)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException("网络未连接!");

        ushort[] unsignedValueToWrite = new ushort[2];
        unsignedValueToWrite[0] = (ushort)value;
        unsignedValueToWrite[1] = (ushort)(value >> 16);
        await _modbusMaster.WriteMultipleRegistersAsync(1, startAddress, unsignedValueToWrite);
    }

    public async Task WriteFloatAsync(ushort startAddress, float value)
    {
        if (_modbusMaster == null)
            throw new InvalidOperationException("网络未连接!");
        byte[] bytes = BitConverter.GetBytes(value);
        ushort[] unsignedValueToWrite = new ushort[2];
        unsignedValueToWrite[0] = BitConverter.ToUInt16(bytes, 0);
        unsignedValueToWrite[1] = BitConverter.ToUInt16(bytes, 2);
        await _modbusMaster.WriteMultipleRegistersAsync(1, startAddress, unsignedValueToWrite);
    }
    #endregion
}
