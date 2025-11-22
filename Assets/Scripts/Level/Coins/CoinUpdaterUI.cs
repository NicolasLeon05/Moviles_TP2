using TMPro;
using UnityEngine;

public class CoinUpdaterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Awake()
    {
        coinsText.text = CurrencySystem.Coins.ToString();
    }

    private void Update()
    {
        coinsText.text = CurrencySystem.Coins.ToString();
    }
}
