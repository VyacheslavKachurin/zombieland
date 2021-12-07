using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuView : MonoBehaviour
{
    public event Action MainMenuClicked;
    public event Action ExitClicked;
    public event Action SaveClicked;
    public event Action ContinueClicked;
    public event Action LoadClicked;

    [SerializeField] private Button _loadButton;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        AssignButtons();

    }
    public void AssignButtons()
    {
        _restartButton.onClick.AddListener(Restart);
        _mainMenuButton.onClick.AddListener(MainMenuButton);
        _exitButton.onClick.AddListener(ExitButton);
        _saveButton.onClick.AddListener(SaveButton);
        _loadButton.onClick.AddListener(LoadButton);
        _continueButton.onClick.AddListener(ContinueButton);

    }
    public void ShowPanel(bool value)
    {
        if (value)
        {
            _animator.SetTrigger("Pause");
        }
        else
        {
            _animator.SetTrigger("Continue");
        }  
    }

    public void GameOver(bool value) //TODO: change name
    {
        _continueButton.interactable = false;
        ShowPanel(value);
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    private void ExitButton()
    {
        ExitClicked();
    }

    private void MainMenuButton()
    {
        MainMenuClicked();
    }

    private void SaveButton()
    {
        SaveClicked();
    }

    private void ContinueButton()
    {
        ContinueClicked();
    }

    private void LoadButton()
    {
        LoadClicked();
    }
}
