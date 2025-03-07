using CommunityToolkit.Mvvm.ComponentModel;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class FirstViewModel : ObservableObject
{
    public FirstViewModel(User user)
    {
        User = user;
    }

    public FirstViewModel() { }

    [ObservableProperty]
    public User _User;
}
