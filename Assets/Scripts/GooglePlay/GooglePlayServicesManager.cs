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

            PlayGamesPlatform.Activate();

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
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            Debug.Log("GPGS Auth result: " + status);
        });
    }

    public bool IsSignedIn()
    {
        return PlayGamesPlatform.Instance.IsAuthenticated();
    }

    // ---------------------------
    //        ACHIEVEMENTS
    // ---------------------------

    public void UnlockAchievement(string id)
    {
        if (!IsSignedIn())
        {
            Debug.LogWarning("No logueado. No se puede desbloquear logro.");
            return;
        }

        PlayGamesPlatform.Instance.UnlockAchievement(id, (success) =>
        {
            Debug.Log("Achievement " + id + " unlocked: " + success);
        });
    }

    public void ShowAchievementsUI()
    {
        if (!IsSignedIn())
        {
            Debug.LogWarning("No logueado, no se puede mostrar UI de achievements.");
            return;
        }

        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    // ---------------------------
    //       LEADERBOARD
    // ---------------------------

    public void SubmitScore(long score)
    {
        if (!IsSignedIn())
        {
            Debug.LogWarning("No logueado. No se puede enviar score.");
            return;
        }

        PlayGamesPlatform.Instance.ReportScore(
            score,
            GPGSIds.leaderboard_leaderboard,
            (success) => Debug.Log("Score enviado: " + success)
        );
    }

    public void ShowLeaderboardUI()
    {
        if (!IsSignedIn())
        {
            Debug.LogWarning("No logueado. No se puede mostrar leaderboard.");
            return;
        }

        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }
}
