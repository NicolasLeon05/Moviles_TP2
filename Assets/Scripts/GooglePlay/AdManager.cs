using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [Header("Controllers")]
    public RewardedController rewardedController;
    public InterstitialController interstitialController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("Google Mobile Ads initialized");

            rewardedController.LoadRewarded();
            interstitialController.LoadInterstitial();
        });
    }

    public void ShowInterstitial()
    {
        interstitialController.ShowInterstitial();
    }

    public void ShowRewarded()
    {
        rewardedController.ShowRewarded();
    }
}
