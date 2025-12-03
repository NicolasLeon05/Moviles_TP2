using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Button buyJumpButton;
    [SerializeField] private TextMeshProUGUI jumpText;
    [SerializeField] private GameObject jumpPrice;

    [SerializeField] private Button buySpeedButton;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private GameObject speedPrice;

    [SerializeField] private int jumpCost = 30;
    [SerializeField] private int speedCost = 30;

    [SerializeField] private Button watchAdButton;

    private void OnEnable()
    {
        RefreshUI();
    }

    void Start()
    {
        buyJumpButton.onClick.AddListener(OnBuyJump);
        buySpeedButton.onClick.AddListener(OnBuySpeed);
        watchAdButton.onClick.AddListener(WatchAdForCoins);

        jumpPrice.GetComponentInChildren<TextMeshProUGUI>().text = jumpCost.ToString();
        speedPrice.GetComponentInChildren<TextMeshProUGUI>().text = speedCost.ToString();


        RefreshUI();
    }

    private void RefreshUI()
    {
        coinsText.text = $"{CurrencySystem.Coins}";

        buyJumpButton.interactable = !CurrencySystem.JumpUpgrade;
        buySpeedButton.interactable = !CurrencySystem.SpeedUpgrade;

        if (CurrencySystem.JumpUpgrade)
        {
            jumpText.text = "Purchased";
            jumpPrice.SetActive(false);
        }

        if (CurrencySystem.SpeedUpgrade)
        {
            speedText.text = "Purchased";
            speedPrice.SetActive(false);
        }

        if(CurrencySystem.SpeedUpgrade && CurrencySystem.JumpUpgrade)
            PlayGamesService.Instance.UnlockAchievement(GPGSIds.achievement_collector);
    }

    private void OnBuyJump()
    {
        if (CurrencySystem.TryBuyJump(jumpCost))
            RefreshUI();
    }

    private void OnBuySpeed()
    {
        if (CurrencySystem.TryBuySpeed(speedCost))
            RefreshUI();
    }

    private void WatchAdForCoins()
    {
        AdManager.Instance.ShowRewarded();
    }
}
