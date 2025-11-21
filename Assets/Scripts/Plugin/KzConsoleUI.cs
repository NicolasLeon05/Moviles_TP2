using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class KzConsoleUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI logText;
    public Button refreshButton;
    public Button clearButton;
    public ScrollRect scrollRect;

    private ILoggerService logger;
    private bool userScrolling = false;

    private void Awake()
    {
        ServiceProvider.TryGetService(out logger);
    }

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshLogs);
        clearButton.onClick.AddListener(ClearLogs);

        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

        RefreshLogs();
    }

    private void OnScrollValueChanged(Vector2 pos)
    {
        userScrolling = scrollRect.verticalNormalizedPosition < 0.999f;
    }

    public void RefreshLogs()
    {
        if (logger == null)
        {
            logText.text = "[Logger not registered]";
            return;
        }

        string logs = logger.GetAll();
        logText.text = string.IsNullOrEmpty(logs) ? "There are no logs" : logs;

        StartCoroutine(ForceScrollToBottom());
    }

    private IEnumerator ForceScrollToBottom()
    {
        yield return null;
        yield return null;

        if (!userScrolling)
            scrollRect.verticalNormalizedPosition = 0f;
    }

    public void ClearLogs()
    {
        if (logger == null)
        {
            Debug.LogWarning("Logger not registered");
            return;
        }

        logger.RequestClear();
        Invoke(nameof(RefreshLogs), 0.5f);
    }
}
