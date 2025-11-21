using UnityEngine;

[DefaultExecutionOrder(-500)]
public class NativeLogForwarder : MonoBehaviour
{
    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }


    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string level = type switch
        {
            LogType.Error => "ERROR",
            LogType.Exception => "ERROR",
            LogType.Assert => "ERROR",
            LogType.Warning => "WARN",
            _ => "INFO"
        };

#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            KzLogger.RecordLog(level, logString, stackTrace);
        }
        catch (System.Exception e)
        {
            // Si algo falla no rompas game
            Debug.LogWarning("NativeLogForwarder: error calling KzLogger: " + e);
        }
#else
#endif
    }
}
