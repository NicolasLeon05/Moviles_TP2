using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialController : MonoBehaviour
{
    private InterstitialAd interstitial;

    private string adUnitId = "ca-app-pub-3940256099942544/1033173712";

    public void LoadInterstitial()
    {
        var request = new AdRequest();

        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Error while loading interstitial: " + error);
                return;
            }

            interstitial = ad;
            RegisterInterstitialEvents();
            Debug.Log("Interstitial loaded");
        });
    }

    public void ShowInterstitial()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial not ready");
        }
    }

    private void RegisterInterstitialEvents()
    {
        interstitial.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial closed");
            interstitial.Destroy();
        };

        interstitial.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Error while showing interstitial: " + error);
        };
    }
}
