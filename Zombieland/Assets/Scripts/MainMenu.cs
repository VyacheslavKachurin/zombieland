using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _loadButton;
    private void Start()
    {
        Initialize();
    }
    public void ExitGame()
    {
        Debug.Log("Exit the game");
        Application.Quit();
    }
    public void ShowSettings()
    {
        _mainMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void ShowLevels()
    {
        Debug.Log("Show levels");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HideSettings()
    {
        _mainMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }
    private void Initialize()
    {
        _playButton.onClick.AddListener(PlayGame);
        _settingsButton.onClick.AddListener(ShowSettings);
        _exitButton.onClick.AddListener(ExitGame);
        _backButton.onClick.AddListener(HideSettings);
        _loadButton.onClick.AddListener(LoadGame);
    }
    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

}
