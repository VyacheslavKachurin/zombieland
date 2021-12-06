using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameModel _gameModelPrefab;

    private GameModel _gameModel;
    private SaveSystem _saveSystem;
    private AudioManager _audioManager;

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

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.PlayMainTheme();
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

        _gameModel = Instantiate(_gameModelPrefab);

        _saveSystem.InsertValues(_gameModel.ExperienceSystem, _gameModel.Player); 
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

        _gameModel = Instantiate(_gameModelPrefab);
    }

    public void NewGame()
    {
        StartCoroutine(NewGameAsync());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        _audioManager.PlayMainTheme();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveGame()
    {
        int sceneName=SceneManager.GetActiveScene().buildIndex;

        Scenes scene = (Scenes)sceneName;       
      
        _saveSystem.SaveGame(_gameModel.ExperienceSystem, _gameModel.Player,scene);
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

        _gameModel = Instantiate(_gameModelPrefab);
    }

}
