using UnityEngine;

public interface IActivatePreviousMenu : IEvent
{
}


public class ActivatePreviousMenu : IActivatePreviousMenu
{
    private GameObject _gameObject;
    private SceneController _sc = ServiceProvider.GetService<SceneController>();

    public GameObject TriggeredByGO => _gameObject;

    public ActivatePreviousMenu(bool activateMenuScene = false)
    {
        _gameObject = null;

        if (activateMenuScene)
            _sc.SetSceneActive(_sc.levelContainer.menusLevel.scenes[0]);
    }
}