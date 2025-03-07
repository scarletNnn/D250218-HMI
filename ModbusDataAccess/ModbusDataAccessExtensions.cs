using Microsoft.Extensions.DependencyInjection;
using ModbusDataAccess.ModelAccess;

namespace ModbusDataAccess;

public static class ModbusDataAccessExtensions
{
    public static void AddModbusDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IModbusAccess, ModbusAccess>();
    }
}
