using Microsoft.Extensions.DependencyInjection;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace SqliteDataAccess;

public static class SqliteDataAccessExtensions
{
    public static void AddSqliteDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IDataAccess, DataAccess>();
        services.AddSingleton<User>();
    }
}
