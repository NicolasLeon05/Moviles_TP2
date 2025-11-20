using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private InputActionReference _pauseAction;
    [SerializeField] private NavigationController _navigationController;
    private SceneController _sceneController = ServiceProvider.GetService<SceneController>();
    private GameManager _gameManager = ServiceProvider.GetService<GameManager>();

    private bool isPaused = false;

    private void OnEnable()
    {
        if (_pauseAction != null)
            _pauseAction.action.performed += OnPause;

        EventProvider.Subscribe<ITogglePause>(OnTogglePauseEvent);
    }

    private void OnDisable()
    {
        if (_pauseAction != null)
            _pauseAction.action.performed -= OnPause;

        EventProvider.Unsubscribe<ITogglePause>(OnTogglePauseEvent);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (_gameManager == null) return;

        if (_sceneController.IsGameplaySceneActive() || isPaused)
            TogglePause();
    }

    private void OnTogglePauseEvent(ITogglePause @event)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        ChangePausedState();

        if (isPaused)
        {
            EventTriggerer.Trigger<IActivateTargetMenu>(new ActivateTargetMenu(new PauseMenuState(), true, true));
            _gameManager.PauseTime();
        }
        else
        {
            _gameManager.ResumeTime();

            _navigationController.SetAllInactive();
            _sceneController.SetSceneActive(_sceneController.PreviousActiveScene);
        }
    }

    private void ChangePausedState()
    {
        isPaused = !isPaused;
    }
}
