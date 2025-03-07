using System.Net;
using SqliteDataAccess.Models;

namespace SqliteDataAccess.DbAccess;

public interface IDataAccess
{
    int AuthenticateUser(NetworkCredential credential);
    Task DeleteUser(int id);
    Task<List<User>> GetAllUsers();
    Task<bool> Init();
    Task InsertUser(string username, string password, int level);
    Task<bool> UserExists(string username);
    Task InsertAlertMessage(AlertMessage msg);
    Task<List<AlertMessage>> GetAlertMessage(DateTime date);
}
