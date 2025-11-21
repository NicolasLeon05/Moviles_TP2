using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KzConsoleUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform contentParent;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button clearButton;
    [SerializeField] private Button refreshButton;

    [Header("Prefabs")]
    [SerializeField] private LogEntryUI logEntryPrefab;

    private ILoggerService logger;

    private void Awake()
    {
        ServiceProvider.TryGetService(out logger);
    }

    private void Start()
    {
        if (clearButton != null)
            clearButton.onClick.AddListener(OnClearRequested);

        if (refreshButton != null)
            refreshButton.onClick.AddListener(RefreshLogs);

        RefreshLogs();
        AddEntry("[TEST] Si ves esto, la UI funciona.");
    }

    public void RefreshLogs()
    {
        if (logger == null)
            return;

        // 1. Eliminar logs viejos del scroll
        foreach (Transform t in contentParent)
            Destroy(t.gameObject);

        string logs = logger.GetAll();

        if (string.IsNullOrEmpty(logs))
        {
            AddEntry("No hay logs.");
        }
        else
        {
            string[] lines = logs.Split('\n');

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                AddEntry(line);
            }
        }

        // Forzar actualización visual y scrollear al final
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private void AddEntry(string text)
    {
        var entry = Instantiate(logEntryPrefab, contentParent);
        entry.SetText(text);
    }

    private void OnClearRequested()
    {
        if (logger == null)
            return;

        // Llama al clear del plugin
        logger.RequestClear();

        // Un pequeño delay hasta que Android borre el archivo
        Invoke(nameof(RefreshLogs), 0.4f);
    }
}
