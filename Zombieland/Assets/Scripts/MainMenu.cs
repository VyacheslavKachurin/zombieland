using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void ExitGame()
    {
        Debug.Log("Exit the game");
        Application.Quit();
    }
    public void ShowSettings()
    {
        Debug.Log("Settings");
    }
    public void ShowLevels()
    {
        Debug.Log("Show levels");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
   
}
