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
        _difficulty=PlayerPrefs.GetInt("Difficulty", 0);
        _blood=PlayerPrefs.GetInt("Blood", 0);
        Initialize();
    }

    private void Initialize()
    {
        _dropDown.value=_difficulty;
        _toggle.isOn = System.Convert.ToBoolean(_blood);

        _dropDown.onValueChanged.AddListener(SetDifficulty);
        _toggle.onValueChanged.AddListener(SetBloodOption);
        
    }

    public void SetDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
    }

    public void SetBloodOption(bool value)
    {
        PlayerPrefs.SetInt("Blood", System.Convert.ToInt32(value));
    }
   
}
