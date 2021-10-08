using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsPanel;
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
   
}
