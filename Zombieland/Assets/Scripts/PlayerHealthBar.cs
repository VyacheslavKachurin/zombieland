using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    private Slider _slider;
    public Gradient Gradient;
    public Image Fill;
    public void SetHealth(int health)
    {
        _slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        Gradient.Evaluate(1f);
        Fill.color = Gradient.Evaluate(1f);
    }
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        SetMaxHealth(100);
    }
    private void Update()
    {
        Fill.color = Gradient.Evaluate(_slider.normalizedValue);
    }

}
