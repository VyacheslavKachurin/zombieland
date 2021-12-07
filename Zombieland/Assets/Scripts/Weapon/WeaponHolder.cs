using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private GameObject _activeWeapon;

    private IWeapon _currentWeapon;

    public void PickUpWeapon(GameObject weapon)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Unequip();
        }
        var newWeapon = Instantiate(weapon);
        newWeapon.transform.parent = _activeWeapon.transform;
        newWeapon.transform.localPosition = new Vector3(0,0, 0.429f);
        newWeapon.transform.localRotation = Quaternion.identity;

        _currentWeapon = newWeapon.GetComponent<IWeapon>();
        OnWeaponChanged(_currentWeapon);
    }
}
