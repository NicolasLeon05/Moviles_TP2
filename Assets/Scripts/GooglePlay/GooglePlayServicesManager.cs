using UnityEngine;
using GooglePlayGames;

public class PlayGamesService : MonoBehaviour
{
    public static PlayGamesService Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
#if UNITY_ANDROID
            PlayGamesPlatform.Activate();
#endif
            SignIn();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ---------------------------
    //        AUTH
    // ---------------------------

    public void SignIn()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            Debug.Log("GPGS Auth result: " + status);
        });
#endif
    }
#if UNITY_ANDROID
    public bool IsSignedIn()
    {

        return PlayGamesPlatform.Instance.IsAuthenticated();
}
#endif

    // ---------------------------
    //        ACHIEVEMENTS
    // ---------------------------

    public void UnlockAchievement(string id)
    {
#if UNITY_ANDROID
        if (!IsSignedIn())
        {
            Debug.LogWarning("User not logued in. Can't unlock achievement");
            return;
        }

        PlayGamesPlatform.Instance.UnlockAchievement(id, (success) =>
        {
            Debug.Log("Achievement " + id + " unlocked: " + success);
        });
#endif
    }

    public void ShowAchievementsUI()
    {
#if UNITY_ANDROID
        if (!IsSignedIn())
        {
            Debug.LogWarning("User not logued in. Can't show achievement UI");
            return;
        }

        PlayGamesPlatform.Instance.ShowAchievementsUI();
#endif
    }

    // ---------------------------
    //       LEADERBOARD
    // ---------------------------

    public void SubmitScore(long score)
    {
#if UNITY_ANDROID
        if (!IsSignedIn())
        {
            Debug.LogWarning("User not logued in. Can't send score");
            return;
        }

        PlayGamesPlatform.Instance.ReportScore(
            score,
            GPGSIds.leaderboard_leaderboard,
            (success) => Debug.Log("Score send: " + success)
        );
#endif
    }

    public void ShowLeaderboardUI()
    {
        #if UNITY_ANDROID
        if (!IsSignedIn())
        {
            Debug.LogWarning("User not logued in. Can't show leaderboard");
            return;
        }

        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
#endif
    }
}
