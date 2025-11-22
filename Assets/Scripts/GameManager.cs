using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level firstLevel;

    private SceneController _sceneController;

    private void Awake()
    {
#if UNITY_EDITOR
        CurrencySystem.ResetForEditor();
#endif

        ServiceProvider.SetService<GameManager>(this);
        #if UNITY_ANDROID && !UNITY_EDITOR
        ServiceProvider.SetService<ILoggerService>(new AndroidLoggerService(), true);
#else
        ServiceProvider.SetService<ILoggerService>(new EditorLoggerService(), true);
#endif
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