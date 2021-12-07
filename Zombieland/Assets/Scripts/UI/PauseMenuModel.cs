using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuModel
{
    private PauseMenuView _view;
    private GameModel _gameModel; // use interface?

    public PauseMenuModel(PauseMenuView view,GameModel gameModel)
    {
        _view = view;
        _gameModel = gameModel;

        _view.MainMenuClicked += OpenMainMenu;
        _view.ExitClicked += Exit;
        _view.SaveClicked += Save;
        _view.ContinueClicked += Continue;
        _view.LoadClicked += Load;

    }

    private void OpenMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Save()
    {
        GameManager.Instance.SaveGame();
    }

    public void Continue()
    {
        _view.ShowPanel(false);
        _gameModel.Continue();
    }

    private void Load()
    {
        GameManager.Instance.LoadGame();
    }

    public void TogglePausePanel(bool value)
    {
        _view.ShowPanel(value);
    }

    public void SetGameOver()
    {
        _view.ShowGameOver(true);
    }

    


}
