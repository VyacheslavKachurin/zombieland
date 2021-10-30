using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HUD : MonoBehaviour
{
    public Button ContinueButton;
    public Button SaveButton;
    public Button LoadButton;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _upgradePanel;

    [SerializeField] private Slider _HPSlider;
    [SerializeField] private Slider _XPSlider;
    [SerializeField] private TextMeshProUGUI _currentXPText;
    [SerializeField] private TextMeshProUGUI _maxXPText;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _bulletText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _weaponIcon;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;


    private float _animatingRate = 0.003f;

    private void Start()
    {
        AssignButtons();

    }

    public void UpdateHealth(float health)
    {
        _HPSlider.value -= health;
    }

    public void Initialize(float maxValue)
    {
        _HPSlider.maxValue = maxValue;
        _HPSlider.value = maxValue;
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

    public void UpgradeMaxHealthValue(float value)
    {
        _HPSlider.maxValue = value;
    }

    public void UpdateXP(int xp)
    {
        StartCoroutine(AnimateXP(xp));
    }
    private IEnumerator AnimateXP(float xp)
    {

        float desiredXP = _XPSlider.value + xp;
        while (_XPSlider.value < desiredXP)
        {
            _currentXPText.text = $"{++_XPSlider.value}";

            yield return new WaitForSeconds(_animatingRate);
        }

    }
    public void UpdateMaxExperience(int xp)
    {
        _XPSlider.value = 0;

        _XPSlider.maxValue = xp;
        _currentXPText.text = "0";
        _maxXPText.text = xp.ToString();

    }
    public void UpdateLevel(int level, int xpToAdd, int maxXP)
    {
        _animatingRate = level / maxXP;
        StartCoroutine(AnimateLevel(level, xpToAdd, maxXP)); //first part of code is repeated

    }
    private IEnumerator AnimateLevel(int level, int xpToAdd, int maxXP)
    {
        while (_XPSlider.value <= _XPSlider.maxValue)
        {
            _currentXPText.text = $"{_XPSlider.value++}";
            yield return new WaitForSeconds(_animatingRate);

            if (_XPSlider.value == _XPSlider.maxValue)
            {
                UpdateMaxExperience(maxXP);
                _levelText.text = $"{level}";
                StartCoroutine(AnimateXP(xpToAdd));
                yield break;
            }
        }
    }
    public void ToggleUpgradePanel()
    {
        _upgradePanel.SetActive(!_upgradePanel.activeInHierarchy);
    }
    public UpgradeDisplay ReturnUpgradePanel()
    {
        return _upgradePanel.GetComponent<UpgradeDisplay>();
    }



}

