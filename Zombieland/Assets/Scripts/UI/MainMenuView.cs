using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuView : MonoBehaviour
{
    public event Action NewGameStarted;
    public event Action SavedGameLoaded;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private SettingsPanel _settingsPanel;

    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _loadButton;
    private void Start()
    {
        Initialize();
    }

    private void ExitGame()
    {
        Debug.Log("Exit the game");
        Application.Quit();
    }

    private void ShowSettings()
    {
        _mainMenuPanel.SetActive(false);
        _settingsPanel.gameObject.SetActive(true);
    }

    private void ShowLevels()
    {
        Debug.Log("Show levels");
    }

    private void NewGame()
    {
        NewGameStarted();        
    }

    private void HideSettings()
    {
        _mainMenuPanel.SetActive(true);
        _settingsPanel.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        _newGameButton.onClick.AddListener(NewGame);
        _settingsButton.onClick.AddListener(ShowSettings);
        _exitButton.onClick.AddListener(ExitGame);
        _backButton.onClick.AddListener(HideSettings);
        _loadButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SavedGameLoaded();   
    }

}
