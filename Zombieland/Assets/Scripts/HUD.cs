using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HUD : MonoBehaviour
{
    public Button ContinueButton; //Action and UnityAction issues

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _armorSlider;
    [SerializeField] private TextMeshProUGUI _bulletText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _weaponIcon;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        AssignButtons();
    }
    public void UpdateHealth(float health)
    {
        _healthSlider.value -= health;
    }
    public void UpdateArmor(float armor)
    {

    }
    public void Initialize(float maxValue)
    {
        _healthSlider.maxValue = maxValue;
        _healthSlider.value = maxValue;
    }
    public void UpdateBullets(int bullets)
    {
        _bulletText.text = $"{bullets}";
    }
    public void PauseGame(bool isPaused)
    {
        _pausePanel.SetActive(isPaused);
        if (isPaused)
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
        _pausePanel.SetActive(isDead);       
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
    public void AssignButtons()
    {
        _restartButton.onClick.AddListener(Restart);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _exitButton.onClick.AddListener(Exit);
    }

    public void UpdateImage(Sprite sprite)
    {
        _weaponIcon.sprite = sprite;
    }
}
