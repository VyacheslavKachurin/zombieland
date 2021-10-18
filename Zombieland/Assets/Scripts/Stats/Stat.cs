using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Stat
{
    public event Action<int> OnValueChanged;

    [SerializeField] private int _baseValue;
    [SerializeField] private int valueChanger;

    private List<int> _modifiers = new List<int>();
    public int GetValue()
    {
        int finalValue = _baseValue;
        _modifiers.ForEach(x => finalValue += x);

        return finalValue;
    }
    public void UpgradeValue()
    {
        _baseValue += valueChanger;
        OnValueChanged(_baseValue);
    }
    public void DowngradeValue()
    {
        _baseValue -= valueChanger;
        OnValueChanged(_baseValue);
    }
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Remove(modifier);
        }
    }

}
