using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.Models;

public partial class Alert : ObservableObject
{
    [ObservableProperty]
    bool _Value;

    [ObservableProperty]
    string _Message;

    partial void OnValueChanged(bool value)
    {
        var dataAccess = App.Current.Services.GetRequiredService<IDataAccess>();
        var msg = new AlertMessage();
        msg.IsActive = value;
        msg.Message = Message;
        msg.Timestamp = DateTime.Now;
        dataAccess.InsertAlertMessage(msg);
    }
}
