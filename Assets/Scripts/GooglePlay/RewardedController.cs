using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedController : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-6791675736047471/5707215684";

    public void LoadRewarded()
    {
        var request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Error al cargar rewarded: " + error);
                return;
            }

            rewardedAd = ad;
            RegisterRewardedEvents();
            Debug.Log("Rewarded cargado!");
        });
    }

    public void ShowRewarded()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("User earned: " + reward.Amount);
                CurrencySystem.AddCoins((int)reward.Amount);
            });
        }
        else
        {
            Debug.Log("Rewarded not ready");
        }
    }

    private void RegisterRewardedEvents()
    {
        rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded closed");
            rewardedAd.Destroy();
            LoadRewarded();
        };
    }
}
