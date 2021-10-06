using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUD : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _armorSlider;
    [SerializeField] private TextMeshProUGUI _bulletText;
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

}
