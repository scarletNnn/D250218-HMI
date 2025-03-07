using SqliteDataAccess.Models;

namespace D250218.Services;

public interface IPermissionService
{
    User CurrentUser { get; }
    void SetCurrentUser(User user);
    void Logout();
    event Action PermissionsChanged;
}
