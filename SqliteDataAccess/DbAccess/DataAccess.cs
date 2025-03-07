using System.Net;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqliteDataAccess.Models;

namespace SqliteDataAccess.DbAccess;

public class DataAccess : IDataAccess
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Init()
    {
        using (MyDbContext ctx = new MyDbContext())
        {
            return await ctx.Database.EnsureCreatedAsync();
        }
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public async Task InsertUser(string username, string password, int level)
    {
        using (MyDbContext ctx = new MyDbContext())
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Level = level
            };
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteUser(int id)
    {
        using (MyDbContext ctx = new MyDbContext())
        {
            User user = ctx.Users.Find(id);
            ctx.Users.Remove(user);
            await ctx.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 查询用户是否存在
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<bool> UserExists(string username)
    {
        using (MyDbContext ctx = new MyDbContext())
        {
            return await ctx.Users.AnyAsync(u => u.Username == username);
        }
    }

    /// <summary>
    /// 查询所有用户
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetAllUsers()
    {
        List<User> users;
        using (MyDbContext ctx = new MyDbContext())
        {
            users = await ctx.Users.ToListAsync();
        }
        return users;
    }

    /// <summary>
    /// 验证用户
    /// </summary>
    /// <param name="credential"></param>
    /// <returns></returns>
    public int AuthenticateUser(NetworkCredential credential)
    {
        int userLevel;
        using (MyDbContext ctx = new MyDbContext())
        {
            var user = ctx.Users.Where(
                u => u.Username == credential.UserName && u.Password == credential.Password
            )
                .FirstOrDefault();
            if (user == null)
            {
                userLevel = 0;
            }
            else
            {
                userLevel = user.Level;
            }
        }
        return userLevel;
    }

    /// <summary>
    /// 插入报警信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public async Task InsertAlertMessage(AlertMessage msg)
    {
        var tableName = DateTime.Now.ToString("yyyMM");
        SearchTime = tableName;

        using (MyDbContext ctx = new MyDbContext { CreateDateTime = tableName })
        {
            //查询是否有表，无表则按月建表
            var sql =
                $@"SELECT Count(*) FROM sqlite_master WHERE type ='table' and name = @tableName ";
            var res = await ctx.Database.SqlQueryRaw<int>(
                sql,
                new SqliteParameter("@tableName", tableName)
            )
                .ToListAsync();
            if (res[0] == 0)
            {
                var sql2 =
                    @$"   CREATE TABLE '{tableName}' (
                          Id INTEGER PRIMARY KEY,
                          IsActive INTEGER NOT NULL CHECK (IsActive IN (0, 1)),
                          Message TEXT NOT NULL,
                          Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
                     )";
                await ctx.Database.ExecuteSqlRawAsync(sql2);
            }

            //插入报警信息
            await ctx.AlertMessages.AddAsync(msg);
            await ctx.SaveChangesAsync();
        }
    }

    public static string SearchTime;

    /// <summary>
    /// 查询报警信息
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<List<AlertMessage>> GetAlertMessage(DateTime date)
    {
        SearchTime = date.ToString("yyyyMM");

        using (MyDbContext ctx = new MyDbContext { CreateDateTime = SearchTime })
        {
            return ctx.AlertMessages.Where(a => a.Timestamp.Day == date.Day).ToList();
        }
    }
}
