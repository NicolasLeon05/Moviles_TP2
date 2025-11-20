
public interface IMenuState
{
    public void Enter(NavigationController controller);
    public void Exit(NavigationController controller);
}

public class MainMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.mainMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.mainMenuGO);
    }
}

public class WinMenuState : IMenuState
{
    private SceneController _sc = ServiceProvider.GetService<SceneController>();
    public void Enter(NavigationController controller)
    {
        _sc.UpdateLastGameplayScene();
        _sc.UnloadNonPersistentScenes();
        controller.ShowMenu(controller.winMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.winMenuGO);
    }
}

public class CreditsMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.creditsMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.creditsMenuGO);
    }
}

public class PauseMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.pauseMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.pauseMenuGO);
    }
}