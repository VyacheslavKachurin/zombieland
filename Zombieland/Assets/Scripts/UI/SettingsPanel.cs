using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDifficulty(int value)
    {
        Debug.Log(_dropDown.value);
    }
   
}
