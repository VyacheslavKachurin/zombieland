using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDown;
    [SerializeField] private Toggle _toggle;

    private int _difficulty;
    private int _blood;
    void Start()
    {
        _difficulty = SettingsSystem.GetDifficulty();
        Initialize();
    }

    private void Initialize()
    {
        _dropDown.value = _difficulty;

        _dropDown.onValueChanged.AddListener(SetDifficulty);
    }

    public void SetDifficulty(int value)
    {


        SettingsSystem.SetDifficulty((Difficulty)value);
    }



}
