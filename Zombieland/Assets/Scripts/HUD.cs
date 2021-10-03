using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _armorSlider;

    private void Start()
    {
       
    }
    public void UpdateHealth(float health)
    {
        _healthSlider.value -= health;
        Debug.Log("got hit");
    }
    public void UpdateArmor(float armor)
    {
       
    }
    public void Initialize(float maxValue)
    {
        _healthSlider.maxValue = maxValue;
        _healthSlider.value = maxValue;
    }
 
}
