using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Model : MonoBehaviour
{
  
    public static  void StartNewGame()
    {
        PlayerPrefs.SetInt("LoadGame", 0); // change to normal system later
        SceneManager.LoadScene(1);
    }

    public static void LoadGame()
    {
        PlayerPrefs.SetInt("LoadGame", 1);  // change to normal system later
        SceneManager.LoadScene(1);
    }

}
