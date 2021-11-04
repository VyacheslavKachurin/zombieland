using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelController _levelControllerPrefab;
    private LevelController _levelController;
    // private LevelController _levelController;

    private SaveSystem _saveSystem;
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        _saveSystem = new SaveSystem();
    }
    private IEnumerator LoadGameAsync()
    {
       Scenes level= _saveSystem.LoadGame();

        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(level.GetName());

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();

        _levelController = Instantiate(_levelControllerPrefab);

        _saveSystem.InsertValues(_levelController.ExperienceSystem, _levelController.Player); 
        //is it okay?

    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameAsync());
    }
    private IEnumerator NewGameAsync()
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();

        _levelController = Instantiate(_levelControllerPrefab);
    }

    public void NewGame()
    {
        StartCoroutine(NewGameAsync());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveGame()
    {
        int sceneName=SceneManager.GetActiveScene().buildIndex;

        Scenes scene = (Scenes)sceneName;       
      
        _saveSystem.SaveGame(_levelController.ExperienceSystem, _levelController.Player,scene);
    }

    public void NextLevel()
    {
            StartCoroutine(LoadNextLevel());
    }
    private IEnumerator LoadNextLevel()
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();

        _levelController = Instantiate(_levelControllerPrefab);
    }

}
