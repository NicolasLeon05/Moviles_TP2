using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level firstLevel;

    private SceneController _sceneController;

    private void Awake()
    {
        ServiceProvider.SetService<GameManager>(this);
        ServiceProvider.SetService<ILoggerService>(new AndroidLoggerService(), true);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _sceneController = ServiceProvider.GetService<SceneController>();
        _sceneController.LoadLevel(firstLevel);
    }

    public void ResetGame()
    {
        _sceneController.UnloadNonPersistentScenes();
        _sceneController.LoadLevel(firstLevel);
    }

    public void PauseTime() => Time.timeScale = 0f;

    public void ResumeTime() => Time.timeScale = 1f;
}