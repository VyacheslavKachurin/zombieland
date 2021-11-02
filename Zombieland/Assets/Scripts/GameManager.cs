using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance=null;
    private void Start()
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
        AsyncOperation asyncLoadLevel=SceneManager.LoadSceneAsync(1);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        
        LevelController.Instance.LoadGame();
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameAsync());
    }
    public void CheckScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log(LevelController.Instance != null);
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
