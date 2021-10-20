using System;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    public event Action<float> OnValueChanged;
    [SerializeField] private float _baseValue;
    [SerializeField] private float _valueChanger;

    public float GetValue()
    {
        return _baseValue;
    }
    public void IncreaseValue()
    {
        _baseValue+=_valueChanger;
        OnValueChanged(_baseValue);
    }
    public void DecreaseValue(float value)
    {
        _baseValue-=_valueChanger;
        OnValueChanged(_baseValue);
    }
 
}
