using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LoadGameAsync()
    {
        // scene is hardcoded, change to different way when have SceneIndex saved in GameData
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1);


        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();

        // LevelController.Instance.LoadGame();
        FindObjectOfType<LevelController>().LoadGame();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameAsync());
    }

    public void NewGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }






}
