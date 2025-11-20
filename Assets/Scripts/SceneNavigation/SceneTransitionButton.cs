using UnityEngine;

public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] private Level _levelToLoad;
    private SceneController _sceneController = ServiceProvider.GetService<SceneController>();
    private GameManager _gameManager = ServiceProvider.GetService<GameManager>();

    /// <summary>
    /// Loads the assigned level, replacing current non-persistent scenes
    /// </summary>
    public void LoadLevel()
    {
        _sceneController.LoadLevel(_levelToLoad);
    }

    /// <summary>
    /// Adds the assigned level to the current set of loaded scenes
    /// </summary>
    public void AddLevel()
    {
        _sceneController.AddLevel(_levelToLoad);
    }

    public void LoadLastLevel()
    {
        if (_sceneController.PreviousActiveLevel != null)
            _sceneController.LoadLevel(_sceneController.PreviousActiveLevel);
        else
            Debug.LogWarning("Previous active level not found");
    }

    public void GoToNextLevel()
    {
        EventTriggerer.Trigger<INextLevelEvent>(new NextLevelEvent());
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        _sceneController.Exit();
    }

    /// <summary>
    /// Unloads all non-persistent scenes and transitions back to the main menu
    /// Also resumes time and shows the cursor.
    /// </summary>
    public void ReturnToMainMenu()
    {
        _gameManager.ResumeTime();
        _sceneController.UnloadNonPersistentScenes();
    }
}