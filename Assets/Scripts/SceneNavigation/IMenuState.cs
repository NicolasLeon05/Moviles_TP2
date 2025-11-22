
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

public class LevelsMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.levelsMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.levelsMenuGO);
    }
}

public class ShopMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.shopMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.shopMenuGO);
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

public class TutorialMenuState : IMenuState
{
    public void Enter(NavigationController controller)
    {
        controller.ShowMenu(controller.tutorialMenuGO, this);
    }

    public void Exit(NavigationController controller)
    {
        controller.HideMenu(controller.tutorialMenuGO);
    }
}