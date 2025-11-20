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
        if (!IsAndroid) return;
        Plugin?.Call("recordLog", level, message, stacktrace);
    }

    public static string GetAllLogs()
    {
        if (!IsAndroid) return "[UNITY EDITOR] No hay logs nativos.";
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
