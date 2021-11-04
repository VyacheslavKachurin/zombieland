using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;

    private void Start()
    {
        Initialize();
    }

    private void LoadNewGame()
    {
        GameManager.Instance.NewGame();
    }

    private void LoadSavedGame()
    {
        GameManager.Instance.LoadGame();
    }

    private void Initialize()
    {
        _mainMenuView = Instantiate(_mainMenuView);
        _mainMenuView.NewGameStarted += LoadNewGame;
        _mainMenuView.SavedGameLoaded += LoadSavedGame;
    }
    private void OnDestroy()
    {
        _mainMenuView.NewGameStarted -= LoadNewGame;
        _mainMenuView.SavedGameLoaded -= LoadSavedGame;

    }

}
