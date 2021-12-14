using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider _slider;

    public void Initialize()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateHealth(float damageAmount)
    {
        _slider.value -= damageAmount;
    }

    public void SetMaxValue(int value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }
}
