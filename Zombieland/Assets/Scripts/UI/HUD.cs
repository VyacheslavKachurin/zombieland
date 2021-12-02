using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HUD : MonoBehaviour
{ 
    [SerializeField] private Slider _HPSlider;
    [SerializeField] private Slider _XPSlider;
    [SerializeField] private Slider _armorSlider;

    [SerializeField] private TextMeshProUGUI _currentXPText;
    [SerializeField] private TextMeshProUGUI _maxXPText;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _bulletText;
    [SerializeField] private Image _weaponIcon;

    private float _animatingRate = 0.003f;

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

    public void UpdateArmorSlider(int value)
    {
        _armorSlider.value = value;
    }
}

