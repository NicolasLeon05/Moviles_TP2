using UnityEngine;

public static class KzLogger
{
    private static AndroidJavaObject _plugin;

    private static bool IsAndroid =>
        Application.platform == RuntimePlatform.Android;

    private static AndroidJavaObject Plugin
    {
        get
        {
            if (!IsAndroid)
            {
                Debug.LogWarning("KzLogger: Plugin called outside Android.");
                return null;
            }

            if (_plugin == null)
            {
                using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    using (var pluginClass = new AndroidJavaClass("com.leon2025.kzloggerplugin.KzLogger"))
                    {
                        _plugin = pluginClass.CallStatic<AndroidJavaObject>("getInstance", activity);
                    }
                }
            }
            return _plugin;
        }
    }

    public static void RecordLog(string level, string message, string stacktrace = "")
    {
        if (!IsAndroid)
        {
#if UNITY_EDITOR
            string path = System.IO.Path.Combine(Application.persistentDataPath, "unity_logs_editor.txt");
            System.IO.File.AppendAllText(path, $"[{System.DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {level}: {message}\n{stacktrace}\n\n");
#endif
            return;
        }
        Plugin?.Call("recordLog", level, message, stacktrace);
    }

    public static string GetAllLogs()
    {
        if (!IsAndroid)
        {
#if UNITY_EDITOR
            string path = System.IO.Path.Combine(Application.persistentDataPath, "unity_logs_editor.txt");
            if (System.IO.File.Exists(path)) return System.IO.File.ReadAllText(path);
            return "[UNITY EDITOR] No hay logs nativos.";
#else
        return "[NOT ANDROID]";
#endif
        }
        return Plugin?.Call<string>("getAllLogs") ?? "";
    }


    public static void RequestClearLogs()
    {
        if (!IsAndroid) return;

        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            Plugin?.Call("requestClearLogs", activity);
        }
    }
}
