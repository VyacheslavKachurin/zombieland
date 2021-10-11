using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public event Action<IWeapon> OnWeaponChanged;
    private int _selectedWeapon;
    private List<GameObject> _weapons = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _selectedWeapon = 0;
       
        
       foreach(Transform weapon in transform)
        {
            _weapons.Add(weapon.gameObject);
            _weapons[_selectedWeapon].SetActive(true);          
        }
        OnWeaponChanged?.Invoke(ChooseWeapon());
    }
    public void ChangeWeapon(bool input)
    {
        int previousSelectedWeapon = _selectedWeapon;
        if (input)
        {
            if(_selectedWeapon>=_weapons.Count-1)
            _selectedWeapon=0;
            else
            {
                _selectedWeapon++;
            }

        }
        else
        {
            if (_selectedWeapon <=0)
            {
                _selectedWeapon = _weapons.Count-1 ;
            }
            else
            {
                _selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != _selectedWeapon)
        {
            _weapons[previousSelectedWeapon].SetActive(false);
            _weapons[_selectedWeapon].SetActive(true);
           
        }
        OnWeaponChanged?.Invoke(ChooseWeapon());
        //add weapon change animation event

    }
    private IWeapon ChooseWeapon()
    {
        
        return _weapons[_selectedWeapon].GetComponent<IWeapon>();
    } 
}
