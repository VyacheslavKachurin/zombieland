using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public event Action<float> OnValueChanged; // add it somewhere

    [SerializeField] private float _baseValue;
    [SerializeField] private float _valueChanger;
    [SerializeField] private string _UIname;


    public float GetValue()
    {
        return _baseValue;
    }
    public void IncreaseValue()
    {
        Debug.Log("value increased in stat.cs");
        _baseValue+=_valueChanger;
        OnValueChanged(_baseValue);
    }
    public void DecreaseValue(float value)
    {
        _baseValue-=_valueChanger;
        OnValueChanged(_baseValue);
    }
    public string GetUIName()
    {
        return _UIname;
    }


 
}
