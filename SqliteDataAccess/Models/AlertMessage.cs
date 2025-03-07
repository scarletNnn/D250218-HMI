namespace SqliteDataAccess.Models;

public class AlertMessage
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
