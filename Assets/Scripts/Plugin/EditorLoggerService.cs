#if UNITY_EDITOR
using System.IO;
using UnityEngine;

public class EditorLoggerService : ILoggerService
{
    private readonly string logPath;

    public EditorLoggerService()
    {
        logPath = Path.Combine(Application.dataPath, "../EditorLogs/logs.txt");

        string dir = Path.GetDirectoryName(logPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    public void Send(string msg)
    {
        File.AppendAllText(logPath, msg + "\n");
    }

    public string GetAll()
    {
        if (!File.Exists(logPath))
            return "";
        return File.ReadAllText(logPath);
    }

    public void RequestClear()
    {
        if (File.Exists(logPath))
            File.Delete(logPath);
    }

    public void Record(string level, string message, string stack = "")
    {
        throw new System.NotImplementedException();
    }
}
#endif
