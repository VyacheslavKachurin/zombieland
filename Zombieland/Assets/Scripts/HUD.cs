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
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _experienceSlider;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _bulletText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _weaponIcon;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;

    private float _animatingSpeed = 0.005f;
    private Coroutine _animatingCoroutine;
    private int _levelCounter=1;

    private void Start()
    {
        AssignButtons();

    }

    public void UpdateHealth(float health)
    {
        _healthSlider.value -= health;
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
    public void UpdateExperienceBar(int experience)
    {

        StartCoroutine(AnimateExperienceBar(experience));
    }
    private IEnumerator AnimateExperienceBar(float amount)
    {
        while (_experienceSlider.value < amount)
        {
            _experienceSlider.value++;
            yield return new WaitForSeconds(_animatingSpeed);
        }
    }

    public void UpdateLevelText(int amountOfTimes,int xp,int maxValue)
    {
        StartCoroutine(AnimateLevelUp(amountOfTimes,xp,maxValue));
    }
    private IEnumerator AnimateLevelUp(int amountOfTimes,int xp,int maxValue)
    {
        for (int i = 0; i < amountOfTimes; i++)
        {
            while (_experienceSlider.value < _experienceSlider.maxValue)
            {
                _experienceSlider.value++;
                yield return new WaitForSeconds(_animatingSpeed);
            }
            _levelCounter++;
            _levelText.text = _levelCounter.ToString();
            _experienceSlider.value = 0;
            _experienceSlider.maxValue = maxValue; 
            //TODO: maxValue must be updated each level;

        }
        StartCoroutine(AnimateExperienceBar(xp));

    }

    public void ToggleInventoryPanel()
    {
        if (_inventoryPanel.activeInHierarchy)
        {
            _inventoryPanel.SetActive(false);
        }
        else
        {
            _inventoryPanel.SetActive(true);
        }
    }
    public void UpgradeMaxHealthValue(int value)
    {
        _healthSlider.maxValue = value;
    }



}

