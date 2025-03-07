using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D250218.Services;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace D250218.ViewModels;

public partial class AlertViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public AlertViewModel(IModbusService modbusService, IDataAccess dataAccess)
    {
        ModbusService = modbusService;
        _dataAccess = dataAccess;
    }

    [ObservableProperty]
    public IModbusService _ModbusService;

    [ObservableProperty]
    DateTime _SelectDate = DateTime.Now;

    ObservableCollection<AlertMessage> Alerts;

    [ObservableProperty]
    ObservableCollection<AlertMessage> _AlertsDisplay;

    /// <summary>
    /// 查询起始时间
    /// </summary>
    [ObservableProperty]
    int _StartHour = 0;

    /// <summary>
    /// 查询结束时间
    /// </summary>
    [ObservableProperty]
    int _EndHour = 24;

    /// <summary>
    /// 按报警信息查询
    /// </summary>
    [ObservableProperty]
    string _FilterText = "";

    partial void OnFilterTextChanged(string value)
    {
        FilterAlertMessage();
    }

    partial void OnStartHourChanged(int value)
    {
        FilterAlertMessage();
    }

    partial void OnEndHourChanged(int value)
    {
        FilterAlertMessage();
    }

    /// <summary>
    /// 过滤报警信息
    /// </summary>
    public void FilterAlertMessage()
    {
        AlertsDisplay = new(
            Alerts.Where(
                a =>
                    a.Message.Contains(FilterText)
                    && a.Timestamp.Hour >= StartHour
                    && a.Timestamp.Hour <= EndHour
            )
        );
    }

    /// <summary>
    /// 查询报警信息
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task SearchAlert()
    {
        try
        {
            var list = await _dataAccess.GetAlertMessage(SelectDate);
            Alerts = new ObservableCollection<AlertMessage>(list);
            AlertsDisplay = Alerts;
        }
        catch (Exception ex)
        {
            Alerts = null;
            MessageBox.Show("Not Found!");
        }
    }
}
