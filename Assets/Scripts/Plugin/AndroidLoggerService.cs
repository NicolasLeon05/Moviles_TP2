using UnityEngine;

public class AndroidLoggerService : ILoggerService
{
    public void Record(string level, string message, string stack = "")
    {
        KzLogger.RecordLog(level, message, stack);
    }

    public string GetAll()
    {
        return KzLogger.GetAllLogs();
    }

    public void RequestClear()
    {
        KzLogger.RequestClearLogs();
    }
}
