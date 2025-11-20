using UnityEngine;
using UnityEngine.UI;

public class KzLoggerUI : MonoBehaviour
{
    [Header("UI References")]
    public Text logText;
    public Button refreshButton;
    public Button clearButton;

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshLogs);
        clearButton.onClick.AddListener(ClearLogs);

        RefreshLogs();
    }

    private void RefreshLogs()
    {
        string logs = KzLogger.GetAllLogs();
        logText.text = string.IsNullOrEmpty(logs) ? "No hay logs." : logs;
    }

    private void ClearLogs()
    {
        KzLogger.RequestClearLogs();
    }
}
