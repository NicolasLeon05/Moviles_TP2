using UnityEngine;

public class FlagGoal : MonoBehaviour
{
    private NavigationController nav;
    private SceneController sceneController;

    private void Awake()
    {
        ServiceProvider.TryGetService(out nav);
        ServiceProvider.TryGetService(out sceneController);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;


        sceneController.UnloadNonPersistentScenes();
        nav.ShowMenu(nav.winMenuGO, new WinMenuState());
        AdManager.Instance.ShowInterstitial();
    }
}
