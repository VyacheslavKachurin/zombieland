using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button ContinueButton;
    public Button SaveButton;
    public Button LoadButton;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Animator _animator;

    private bool _isPaused = false;

    private void Start()
    {
        AssignButtons();

    }
    public void AssignButtons()
    {
        _restartButton.onClick.AddListener(Restart);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _exitButton.onClick.AddListener(Exit);
    }
    public void ShowPanel(bool value)
    {
        _isPaused = value;
        if (_isPaused)
        {
            _animator.SetTrigger("Pause");
        }
        else
        {
            _animator.SetTrigger("Continue");
        }
        
    }
    public void GameOver(bool isDead)
    {
        ContinueButton.interactable = false;
        gameObject.SetActive(isDead);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowSettings()
    {
        Debug.Log("Show Settings");
    }
}
