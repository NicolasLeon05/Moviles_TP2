using UnityEngine;

public class GooglePlayButton : MonoBehaviour
{
    public void ShowAchievementsUI()
    {
        PlayGamesService.Instance.ShowAchievementsUI();
    }

    public void ShowLeaderboardUI()
    {
        PlayGamesService.Instance.ShowLeaderboardUI();
    }
}
