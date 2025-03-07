using System.Collections.ObjectModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class FirstViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public FirstViewModel(User user, IDataAccess dataAccess)
    {
        User = user;
        _dataAccess = dataAccess;
        Init();
    }

    public FirstViewModel() { }

    [ObservableProperty]
    public User _User;

    [ObservableProperty]
    private ObservableCollection<int> _ProductionCount = new();

    [ObservableProperty]
    DateTime _SelectDate = DateTime.Now;

    [ObservableProperty]
    int _SumCount;

    [ObservableProperty]
    int _DarkCount;

    [ObservableProperty]
    int _LightCount;

    private void Init()
    {
        for (ushort i = 0; i < 24; i++)
        {
            ProductionCount.Add(0);
        }
    }

    [RelayCommand]
    async Task ShowProductionCount()
    {
        try
        {
            for (ushort i = 0; i < 24; i++)
            {
                ProductionCount[i] = await _dataAccess.GetProductionMessage(SelectDate, i);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        CalculateCount();
    }

    private void CalculateCount()
    {
        LightCount = 0;
        DarkCount = 0;
        SumCount = 0;
        for (ushort i = 8; i < 20; i++)
        {
            LightCount = LightCount + ProductionCount[i];
        }
        for (ushort i = 0; i < 24; i++)
        {
            SumCount = SumCount + ProductionCount[i];
        }
        DarkCount = SumCount - LightCount;
    }

    [RelayCommand]
    async Task Add()
    {
        var msg = new ProductionMessage();
        msg.Value = true;
        msg.Timestamp = DateTime.Now;
        await _dataAccess.InsertProductionMessage(msg);
    }
}
