using TMPro;
using UnityEngine;

public class LogEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    public void SetText(string msg)
    {
        messageText.text = msg;
    }
}