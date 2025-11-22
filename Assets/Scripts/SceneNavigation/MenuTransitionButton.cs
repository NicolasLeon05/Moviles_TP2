using UnityEngine;
using UnityEngine.UI;

public class MenuTransitionButton : MonoBehaviour
{
    private NavigationController _navigationController;

    private void Start()
    {
        ServiceProvider.TryGetService(out _navigationController);
    }

    public void ActivateBaseMenu()
    {
        _navigationController.GoToMenu(new MainMenuState());
    }

    public void ActivateLevelsMenu()
    {
        _navigationController.GoToMenu(new LevelsMenuState());
    }

    public void ActivateShopMenu()
    {
        _navigationController.GoToMenu(new ShopMenuState());
    }

    public void ActivateTutorialMenu()
    {
        _navigationController.GoToMenu(new TutorialMenuState());
    }


    public void ButtonSetPreviousActive()
    {
        _navigationController.GoToMenu(_navigationController.PreviousMenu);
    }

    /// <summary>
    /// Calls the SetAllInactive() function from NavigationController.
    /// Sets the GameManager state to the assigned target state
    /// </summary>
    public void ButtonSetAllInactive()
    {
        EventTriggerer.Trigger<ISetAllMenusInactive>(new SetAllMenusInactive());
    }

    public void ButtonTogglePause()
    {
        EventTriggerer.Trigger<ITogglePause>(new TogglePauseEvent());
    }

    

    public void ExitApp()
    {
        Application.Quit();
    }
}
